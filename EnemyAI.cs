using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Enemy : MonoBehaviour
{
    #region Variables
    [Header("States")]
    [ShowOnly][SerializeField] private string currentStateName = "Wander"; // Only for visualization inside the inspector
    private IEnemyState currentState;
    private IEnemyState previousState;
    private WanderState wanderState;
    private InvestigateState investigateState;
    private ChaseState chaseState;
    private SearchState searchState;
    private RecoverState recoverState;
    private StalkState stalkState;

    [Header("Movement Speeds")]
    [SerializeField] private float wanderSpeed = 6f;
    [SerializeField] private float investigateSpeed = 8f;
    [SerializeField] private float chaseSpeed = 10f; // maybe 12
    [SerializeField] private float recoverSpeed = 4f; 

    [Header("Wander State")]
    [SerializeField][Tooltip("Max duration in seconds of the wander state")] private float wanderDuration = 45f;
    [SerializeField][Tooltip("The radius where the enemy can choose a point to wander")] private float wanderCircleRadius = 30f;
    [SerializeField][Tooltip("How often will the enemy bias toward player location (percentage)")] private float biasChancePercentage = 60;
    [SerializeField][Tooltip("Accuracy radius for biasing toward the player")] private float biasAccuracy = 20f;

    [Header("Stalk State")]
    [SerializeField][Tooltip("Max approach distance from the enemy to the player")] private float stalkingDistance = 15f;
    [SerializeField][Tooltip("Duration in seconds of the stalk state")] private float stalkDuration = 25f;

    [Header("Chase State")]
    [SerializeField][Tooltip("Max chase duration in seconds")] private float maxChaseDuration = 20;
    [SerializeField][Tooltip("Min chase duration in seconds")] private float minChaseDuration = 15;
    [SerializeField] private float catchDistance = 1.5f;

    [Header("Search State")]
    [SerializeField][Tooltip("The radius where the enemy can choose a point to search")] private float searchCircleRadius = 15f;
    [SerializeField][Tooltip("Time to search for the player after losing sight")] private float searchDuration = 10f; 
    
    [Header("Ambush State")]
    [ShowOnly][SerializeField] private bool canAmbush = false;
    [SerializeField][Tooltip("Max time in seconds that the enem will be waiting if the player doesn't come close enough in time")] private float maxAmbushDuration = 25f;
    [SerializeField][Tooltip("Min time in seconds that the enem will be waiting if the player doesn't come close enough in time")] private float minAmbushDuration = 10f;
    [SerializeField][Tooltip("Time in seconds that the enemy will wait when player is walking away from it")] private float walkAwayTimer = 5f; 
    [SerializeField] private Transform[] ambushPositions;
    [SerializeField] private float ambushActivationRange = 10f;
    [SerializeField] private float ambushOpportunityRange = 40f;
    [SerializeField][Tooltip("Allow for ambush every n seconds")] private float ambushTimer = 60;

    [Header("Recover State")]
    [SerializeField][Tooltip("Duration in seconds of the recover state")] private float recoverDuration = 15f;

    [Header("Vision")]
    [SerializeField] private Transform[] eyes;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private float viewAngle = 60f;
    [SerializeField][Tooltip("Time required to confirm seeing the player")] private float timeToSpotPlayer = 1f;

    [Header("Timers")]
    [SerializeField][Tooltip("Time to give up chase after losing sight")] private float chaseLoseTime = 3f;

    [Header("Intensity Increase")]
    [SerializeField][Tooltip("Min recover duration after intensity increase")] private float minRecoverDuration = 5f;
    [SerializeField][Tooltip("Max chase duration allowed after intensity increase")] private float maxMaxChaseDuration = 25f;
    [SerializeField][Tooltip("Max min chase duration allowed after intensity increase")] private float maxMinChaseDuration = 20f;
    [SerializeField][Tooltip("Max view distance allowed after intensity increase")] private float maxViewDistance = 20f;
    [SerializeField][Tooltip("Min time to spot player allowed after intensity increase")] private float minTimeToSpotPlayer = 0.5f;
    [SerializeField][Tooltip("Max wander speed allowed after intensity increase")] private float maxWanderSpeed = 8f;
    [SerializeField][Tooltip("Max chase speed allowed after intensity increase")] private float maxChaseSpeed = 12f;
    [SerializeField][Tooltip("Max investigate speed allowed after intensity increase")] private float maxInvestigateSpeed = 10f;
    [SerializeField][Tooltip("Max bias chance percentage allowed after intensity increase")] private float maxBiasChancePercentage = 75f;
    [SerializeField][Tooltip("Min bias accuracy allowed after intensity increase")] private float maxBiasAccuracy = 10f;

    // -- Private variables -- 
    private Transform player;
    private float _playerVisibleTimer = 0f;
    private float _chaseLoseTimer = 0f;
    private Vector3 _playerLastKnownPosition;
    private Vector3 _investigateTarget;
    private Vector3 _previousPosition;
    private float _ambushTimer = ambushTimer;

    // -- Components --
    [Header("A* Pathfinding")]
    [SerializeField] private AIDestinationSetter aIDestinationSetter;
    [SerializeField] private AIPath aIPath;
    private Transform _moveTarget;

    // Pooling support
    // private static Stack<Transform> _moveTargetPool = new Stack<Transform>();
    // private const string MoveTargetName = "Pooled_Enemy_MoveTarget";

    [Header("Flashlight")]
    [SerializeField][Tooltip("Reference to the player's flashlight toggle script")] private FlashlightToggle playerFlashlight;
    
    [Header("Enemy Chase Spotlight")]
    [SerializeField] private GameObject enemySpotlight;

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private bool showNoiseGizmos = true;
    [SerializeField] private bool showViewDistance = true;
    [SerializeField] private bool showLineToPLayer = true;
    [SerializeField] private bool showViewAngle = true;
    [SerializeField] private bool showWanderRadius = true; // wander bias bypasses this range
    [SerializeField] private bool showBiasAccuracy = true;
    [SerializeField] private bool showCatchDistance = true;
    [SerializeField] private bool showStalkingDistance = true;
    [SerializeField] private bool showAmbushActivationRange = true;
    [SerializeField] private bool showAmbushOpportunityRange = true;
    [SerializeField] private bool showSearchCircleRadius = true;

    private Vector3 _noisePosGizmos;
    private float _noiseRadiusGizmos;

#endif
    #endregion

    #region Unity Methods

    private void Awake()
    {
        // // Try to reuse one from the pool; if none, create new
        // if (_moveTargetPool.Count > 0)
        // {
        //     _moveTarget = _moveTargetPool.Pop();
        //     _moveTarget.SetParent(transform);
        //     _moveTarget.gameObject.SetActive(true);
        // }
        // else
        // {
        //     _moveTarget = new GameObject(MoveTargetName).transform;
        //     _moveTarget.SetParent(transform);
        // }

        //_moveTarget.position = transform.position;
        
        _moveTarget = new GameObject("Enemy_MoveTarget").transform;
        _moveTarget.SetParent(transform);
        var avoidWarning = currentStateName; // This is just to avoid the warning of "currentStateName" not being used
    }

    private void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        enemySpotlight.SetActive(false);
        previousPosition = player.position;

        // Initialize states
        InitializeStates();

        currentState = wanderState;
        currentState.Enter();

        // Subscribe to events
        NoiseEventManager.OnNoise += HandleNoiseEvent;
    }

    private void Update()
    {
        currentState.Update();

        if(_ambushTimer == ambushTimer)
        {
            canAmbush = true;
        } 
        else
        {
            _ambushTimer += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to events
        NoiseEventManager.OnNoise -= HandleNoiseEvent;

        // if (_moveTarget != null)
        // {
        //     _moveTarget.gameObject.SetActive(false);
        //     _moveTargetPool.Push(_moveTarget);
        // }
    }

    private void OnDisable()
    {
        // Unsubscribe to events
        NoiseEventManager.OnNoise -= HandleNoiseEvent;
    }
    #endregion

    #region Initialization
    private void InitializeStates()
    {
        wanderState = new WanderState(this);
        investigateState = new InvestigateState(this);
        chaseState = new ChaseState(this);
        searchState = new SearchState(this);
        recoverState = new RecoverState(this);
        stalkState = new StalkState(this);
    }
    #endregion

    #region Noise Handling
    private void HandleNoiseEvent(Vector3 noisePos, float radius)
    {
        // If in a chase...
        if (currentState == chaseState) return; // Ignore noise

        float dist = Vector3.Distance(transform.position, noisePos);

        // Else...
        if(dist < viewDistance) // if within view distance
        {
            ChangeState(chaseState); // Chase the player
        }
        else if (dist <= radius) // if outside view distance but inside noise radius
        {
            _investigateTarget = noisePos;
            ChangeState(investigateState);
#if UNITY_EDITOR
            _noisePosGizmos = noisePos;
            _noiseRadiusGizmos = radius;
#endif
        }
    }
    #endregion

    #region Sight Detection
    public bool CanSeePlayer()
    {
        // If flashlight not on, ignore sight detection
        if (!playerFlashlight.isOn && currentState != chaseState)
            return false;

        // Try to see the player from each eye
        foreach (var eye in eyes)
        {
            Vector3 toPlayer = player.position - eye.position;
            float dist = toPlayer.magnitude;
            if (dist > viewDistance) 
                continue;   // too far for this eye

            Vector3 dir = toPlayer / dist; // normalize once

            // FOV check
            if (Vector3.Angle(eye.forward, dir) > viewAngle * 0.5f)
                continue;   // outside this eye’s cone

            // Occlusion check
            if (Physics.Raycast(eye.position, dir, out var hit, viewDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    _playerLastKnownPosition = player.position;
                    return true;
                }
            }
        }

        // no eye had a clear view
        return false;
    }

    #endregion

    #region Tension Cycle
    private void IncreaseIntensity()
    {
        recoverDuration = Mathf.Max(recoverDuration - 2f, minRecoverDuration); // min 5 sec
        maxChaseDuration = Mathf.Min(maxChaseDuration + 2f, maxMaxChaseDuration); // max 25 sec
        minChaseDuration = Mathf.Min(minChaseDuration + 2f, maxMinChaseDuration); // max 20 sec

        timeToSpotPlayer = Mathf.Max(timeToSpotPlayer - 0.1f, minTimeToSpotPlayer); // max 0.5 sec
        viewDistance = Mathf.Min(viewDistance + 2f, maxViewDistance); // max 20 units

        wanderSpeed = Mathf.Min(wanderSpeed + 0.5f, maxWanderSpeed); // max 8
        chaseSpeed = Mathf.Min(chaseSpeed + 0.2f, maxChaseSpeed); // max 12
        investigateSpeed = Mathf.Min(investigateSpeed + 0.5f, maxInvestigateSpeed); // max 10

        biasChancePercentage = Mathf.Min(biasChancePercentage + 2f, maxBiasChancePercentage); // max 75%
        biasAccuracy = Mathf.Min(biasAccuracy - 1f, maxBiasAccuracy); // 10 units
    }
    #endregion

    #region State Transitions
    public void ChangeState(IEnemyState newState)
    {
        currentState.Exit();
        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }
    #endregion

    #region State Logic
    public bool CheckPlayerSight()
    {
        // If can see the player...
        if (CanSeePlayer())
        {
            _playerVisibleTimer += Time.deltaTime;
            if (_playerVisibleTimer >= timeToSpotPlayer)
            {
                _playerVisibleTimer = 0f;
                return true; // Confirm after timer
            }
        }
        else
        {
            _playerVisibleTimer = 0f;
        }
        return false;
    }

    public bool CheckLosePlayerSight()
    {
        // If can't see the player...
        if (!CanSeePlayer())
        {
            _chaseLoseTimer += Time.deltaTime;
            if (_chaseLoseTimer >= chaseLoseTime)
            {
                _chaseLoseTimer = 0f;
                return true;  // Lose the player
            }
        }
        else
        {
            _chaseLoseTimer = 0f;
        }
        return false;
    }

    // Movement helpers
    public Vector3 SetNewWanderTarget()
    {
        // Choose a random point on A* grap within a radius
        Vector2 randCircle = Random.insideUnitCircle * wanderCircleRadius;
        Vector3 target = transform.position + new Vector3(randCircle.x, 0, randCircle.y);

        // Bias occasionally towards player region 
        var bias = biasChancePercentage / 100;
        if (Random.value < bias) {
            Vector3 biasPoint = player.position + Random.insideUnitSphere * biasAccuracy;
            biasPoint.y = transform.position.y;
            return biasPoint;
        }

        return target;
    }

    public Vector3 SetSearchPlayerTarget()
    {
        Vector3 searchPoint = _playerLastKnownPosition + Random.insideUnitSphere * searchCircleRadius;
        searchPoint.y = transform.position.y;
        return searchPoint;
    }

    public void MoveTowards(Vector3 target, float speed)
    {
        aIPath.maxSpeed = speed;

        _moveTarget.position = target;

        aIDestinationSetter.target = _moveTarget;
    }

    public void CatchPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= catchDistance + 5)
        {
            aIPath.rotation = Quaternion.LookRotation(player.position - transform.position); 
        }

        if (Vector3.Distance(transform.position, player.position) <= catchDistance)
        {
            GameManager.Die();
        }
    }

    public bool CheckForAmbushOpportunity()
    {
        if(Vector3.Distance(gameObject.transform, player) > ambushOpportunityRange)
            return false;
        
        Vector3 currentPosition = player.position;
        Vector3 directionToTarget = (target.position - currentPosition).normalized;
        Vector3 playerMovement = (currentPosition - _previousPosition).normalized;
        
        float dotProduct = Vector3.Dot(directionToTarget, playerMovement);

        _previousPosition = currentPosition;

        if (dotProduct > 0) // Player is moving towards the target
        {
            return true;
        }
        else if (dotProduct < 0) // Player is moving away from the target
        {
            return false;
        }
        else // Player is moving perpendicular to the target
        {
            return false;
        }
    }

    public void ResetAmbushTimer()
    {
        _ambushTimer = 0;
        canAmbush = false;
    }

    public Transform FindNearestTransform(Transform self, Transform[] targets)
    {
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(self.position, target.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }

    // Accessors for target data
    #region Accessors
    public Transform Player() => player;
    public float WanderSpeed() => wanderSpeed;
    public float InvestigateSpeed() => investigateSpeed;
    public float ChaseSpeed() => chaseSpeed;
    public float RecoverSpeed() => recoverSpeed;
    public Vector3 InvestigateTarget() => _investigateTarget;
    public float SearchDuration() => searchDuration;
    public float RecoverDuration() => recoverDuration;
    public float WanderDuration() => wanderDuration;
    public float StalkDuration() => stalkDuration;
    public AIPath AIPath() => aIPath;
    #endregion
    #endregion

    #region States
    // State classes for FSM

    /// <summary>
    /// 
    /// </summary>

    #region Wander State
    // Wanders around randomly with a 60% chance to bias towards the player 
    private class WanderState : IEnemyState
    {
        private Enemy enemy;
        public WanderState(Enemy enemy) { this.enemy = enemy; }

        private AIPath aiPath;
        private Vector3 wanderTarget;
        private float timer;

        public void Enter()
        {
            aiPath = enemy.AIPath();
            aiPath.endReachedDistance = 2.5f;
            enemy.currentStateName = "Wander";
            wanderTarget = enemy.SetNewWanderTarget();
        }

        public void Update()
        {
            timer += Time.deltaTime;

            // if wander timer runs out, stalk
            if(timer > enemy.WanderDuration()) 
            {
                timer = 0;
                enemy.ChangeState(enemy.stalkState);
            }

            if(canAmbush && enemy.CheckForAmbushOpportunity)
            {
                enemy.ChangeState(enemy.ambushState);
            }

            // Check for sight
            if (enemy.CheckPlayerSight())
            {
                enemy.ChangeState(enemy.chaseState);
            }

            // Move towards target
            enemy.MoveTowards(wanderTarget, enemy.WanderSpeed());

            // If reached target, pick a new one
            if (enemy.aIPath.reachedEndOfPath)
            {
                wanderTarget = enemy.SetNewWanderTarget();
            } 
        }

        public void Exit()
        {

        }
    }
    #endregion

    #region Stalk State
    // Stalk the player from distance until the stalk timer runs out
    private class StalkState : IEnemyState
    {
        private Enemy enemy;
        public StalkState(Enemy enemy) { this.enemy = enemy; }

        private float timer;
        private AIPath aiPath;

        public void Enter()
        {
            aiPath = enemy.AIPath();
            aiPath.endReachedDistance = enemy.stalkingDistance;
            enemy.currentStateName = "Stalk";
        }

        public void Update()
        {
            Transform player = enemy.Player();
            timer += Time.deltaTime;

            enemy.MoveTowards(player.position, enemy.InvestigateSpeed());

            // if stalk timer runs out, wander
            if(timer > enemy.StalkDuration())
            {
                timer = 0;
                enemy.ChangeState(enemy.wanderState);
            }

            if(canAmbush && enemy.CheckForAmbushOpportunity)
            {
                enemy.ChangeState(enemy.ambushState);
            }

            // Check for sight
            if (enemy.CheckPlayerSight())
            {
                enemy.ChangeState(enemy.chaseState);
            }
        }  

        public void Exit()
        {
            aiPath.endReachedDistance = 1.2f;
        }
    }
    #endregion

    #region Investigate State
    // Quickly go investigate a noise
    private class InvestigateState : IEnemyState
    {
        private Enemy enemy;
        public InvestigateState(Enemy enemy) { this.enemy = enemy; }

        private Vector3 noisePos;
        private float arriveThreshold = 2f;
        private AIPath aiPath;

        public void Enter()
        {
            noisePos = enemy.InvestigateTarget();
            enemy.currentStateName = "Investigate";
            aiPath = enemy.AIPath();
            aiPath.endReachedDistance = 0.2f;
        }

        public void Update()
        {
            // Check for noise or sight
            if (enemy.CheckPlayerSight())
            {
                enemy.ChangeState(enemy.chaseState);
                return;
            }

            float dist = Vector3.Distance(enemy.transform.position, noisePos);

            // If it arrived, immediately return to wandering
            if (dist <= arriveThreshold)
            {
                enemy.ChangeState(enemy.wanderState);
                return;
            }

            // Otherwise, keep moving toward the noise spot
            enemy.MoveTowards(noisePos, enemy.ChaseSpeed());
        }

        public void Exit()
        {

        }
    }
    #endregion

    #region Chase State
    // Chase the player until the chase timer runs out
    private class ChaseState : IEnemyState
    {
        private Enemy enemy;
        public ChaseState(Enemy enemy) { this.enemy = enemy; }

        private float chaseDuration;
        private float chaseTimer;
        private AIPath aiPath;

        public void Enter()
        {
            aiPath = enemy.AIPath();
            aiPath.endReachedDistance = 0.5f;
            enemy.currentStateName = "Chase";
            chaseDuration = Random.Range(enemy.minChaseDuration, enemy.maxChaseDuration);
            enemy.enemySpotlight.SetActive(true);
        }

        public void Update()
        {
            Transform player = enemy.Player();
            chaseTimer += Time.deltaTime;

            // Move towards player
            enemy.MoveTowards(player.position, enemy.ChaseSpeed());
            
            // If player lost, search
            if (enemy.CheckLosePlayerSight())
            {
                chaseTimer = 0;
                enemy.ChangeState(enemy.searchState);
            }

            // If chase timer runs out, recover
            if(chaseTimer > chaseDuration)
            {
                chaseTimer = 0;
                enemy.ChangeState(enemy.recoverState);
            }

            // Catch player code to only catch when on a chase
            enemy.CatchPlayer();
        }

        public void Exit()
        {
            enemy.enemySpotlight.SetActive(false);
        }
    }
    #endregion
    
    #region Search State
    // Search for the player 
    private class SearchState : IEnemyState
    {
        private Enemy enemy;
        public SearchState(Enemy enemy) { this.enemy = enemy; }
        private Vector3 searchTarget; 
        private float timer = 0f;
        private AIPath aiPath;  

        public void Enter()
        {
            aiPath = enemy.AIPath();
            aiPath.endReachedDistance = 4f;
            enemy.currentStateName = "Search";
            searchTarget = enemy.SetSearchPlayerTarget();
        }

        public void Update()
        {
            // Move towards target
            enemy.MoveTowards(searchTarget, enemy.ChaseSpeed());

            timer += Time.deltaTime;

            // If reached target, pick a new one
            if (enemy.aIPath.reachedEndOfPath)
            {
                searchTarget = enemy.SetSearchPlayerTarget();
            }

            // Check for noise or sight
            if (enemy.CheckPlayerSight())
            {
                enemy.ChangeState(enemy.chaseState);
            }
            
            // If timer is up, recover
            if(timer >= enemy.SearchDuration())
            {
                enemy.ChangeState(enemy.recoverState);
                timer = 0f;
            }
        }

        public void Exit()
        {

        }
    }
    #endregion

    #region Ambush State
    // state where the enemy sets up an ambush when it notices the player moving towards it

    // maybe aadd a smoothing to player's movement in calculations
    // add a check to see if the player sees the spider during the ambush, if so, chase
    private class AmbushState : IEnemyState 
    {
        private Enemy enemy;
        public AmbushState(Enemy enemy) { this.enemy = enemy; }

        private float ambushDuration = Random.Range(minAmbushDuration, maxAmbushDuration); // randomize waiting time
        private Transform nearestTarget;
        private bool isAmbushing = false;
        private float timer = 0f;
        private float timer2 = 0f;

        public Enter()
        {
            enemy.ResetAmbushTimer();
            nearestTarget = enemy.FindNearestTransform(gameObject.transform, ambushPositions); // find nearest ambush position

            if(Vector3.Distance(transform.position, nearestTarget.position) < 15f) // checks to see if ambush pos is close enough
            {
                enemy.MoveTowards(nearestTarget.position, enemy.GetChaseSpeed()); // if yes, move to it
                isAmbushing = true;
            }
            else
            {
                enemy.ChangeState(enemy.previousState) // if not, return to last state
            }
        }

        public Update()
        {
            if(!isAmbushing) return;

            if(!enemy.CheckForAmbushOpportunity()) // if player walks away...
            {
                timer2 = 0f;

                if(enemy.walkAwayTimer > timer) // wait to see if player will come back
                    timer += Time.deltaTime;
                else 
                    enemy.ChangeState(enemy.previousState); // return to previous state
            }
            else // if player doesn't walk away
            {
                timer = 0f;

                if(ambushDuration > timer2) // wait to see if player comes close enough for spider to see it
                    {
                        timer2 += Time.deltaTime;
                        if(Vector3.Distance(gameObject.transform, enemy.player) <= ambushActivationRange)
                            enemy.ChangeState(enemy.chaseState);
                    }
                else
                    enemy.ChangeState(enemy.stalkState); // if not goes into stalk state
            }
        }

        public Exit() 
        { 
            enemy.ResetAmbushTimer();
        }
    }
    #endregion

    #region Recover State
    // Recovers after a chase, slowly wandering around for a while
    private class RecoverState : IEnemyState
    {
        private Enemy enemy;
        public RecoverState(Enemy enemy) { this.enemy = enemy; }
        private float timer;
        private Vector3 wanderTarget;
        private AIPath aiPath;

        public void Enter()
        {
            aiPath = enemy.AIPath();
            aiPath.endReachedDistance = 1.2f;
            enemy.currentStateName = "Recover";
            wanderTarget = enemy.SetNewWanderTarget();
        }

        public void Update()
        {
            timer += Time.deltaTime;

            // Move towards target
            enemy.MoveTowards(wanderTarget, enemy.RecoverSpeed());

            // If reached target, pick a new one
            if (enemy.aIPath.reachedEndOfPath)
            {
                wanderTarget = enemy.SetNewWanderTarget();
            } 

            if(enemy.CheckPlayerSight())
            {
                enemy.ChangeState(enemy.chaseState);
            }
            
            // if recover duration is over...
            if(timer > enemy.RecoverDuration())
            {
                enemy.ChangeState(enemy.wanderState); // go back to wander state
                timer = 0f;
            }
        }

        public void Exit()
        {
            enemy.IncreaseIntensity();
        }
    }
    #endregion
    #endregion

     #region Debug Gizmos
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Vector3 center = transform.position;
        Vector3 playerCenter = player.position;
        Vector3 lastKnownPlayerPos = _playerLastKnownPosition;

        switch (currentStateName)
        {
            case "Wander":
                DrawWanderGizmos(center, playerCenter);
                break;
            case "Chase":
                DrawChaseGizmos(center);
                break;
            case "Ambush":
                DrawAmbushGizmos(center);
                break;
            case "Stalk":
                DrawStalkGizmos(playerCenter);
                break;
            case "Search":
                DrawSearchGizmos(lastKnownPlayerPos);
                break;
        }

        if(showViewDistance)
        {
            DrawViewGizmos(center, playerCenter)
        }

        if (showNoiseGizmos)
        {
            DrawNoiseGizmos();
        }
    }

    private void DrawWanderGizmos(Vector3 center, Vector3 playerCenter)
    {
        if (showWanderRadius)
        {
            Gizmos.color = Color.magenta;
            DrawCircleXZ(center, wanderCircleRadius, 64);
        }

        if(showBiasAccuracy)
        {
            Gizmos.color = Color.blue;
            DrawCircleXZ(playerCenter, biasAccuracy, 64);
        }
    }

    private void DrawChaseGizmos(Vector3 center)
    {
        if (showCatchDistance)
        {
            Gizmos.color = Color.red;
            DrawCircleXZ(center, catchDistance, 64);
        }
    }

    private void DrawAmbushGizmos(Vector3 center)
    {
        if (showAmbushActivationRange)
        {
            Gizmos.color = Color.blue;
            DrawCircleXZ(center, ambushActivationRange, 64);
        }
        if (showAmbushOpportunityRange)
        {
            Gizmos.color = Color.cyan;
            DrawCircleXZ(center, ambushOpportunityRange, 64);
        }
    }

    private void DrawStalkGizmos(Vector3 playerCenter)
    {
        if (showStalkingDistance)
        {
            Gizmos.color = Color.green;
            DrawCircleXZ(playerCenter, stalkingDistance, 64);
        }
    }

    private void DrawSearchGizmos(Vector3 lastKnownPlayerPos)
    {
        if (showSearchCircleRadius)
        {
            Gizmos.color = Color.yellow;
            DrawCircleXZ(lastKnownPlayerPos, searchCircleRadius, 64);
        }
    }

    private void DrawViewGizmos(Vector3 center, Vector3 playerCenter)
    {
        Gizmos.color = Color.green;
        DrawCircleXZ(center, viewDistance, 64);
        
        if(showLineToPLayer)
        {
            if (player != null)
            {
                Vector3 toPlayer = playerCenter - center;
                toPlayer.y = 0;
                float dist = toPlayer.magnitude;
                float angle = Vector3.Angle(forward, toPlayer.normalized);

                // Change color when seeing the player
                if (dist <= viewDistance && angle <= halfAngle)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.white;

                Gizmos.DrawLine(center, center + toPlayer);
            }
        }

        if (showViewAngle)
        {
            Gizmos.color = Color.cyan;
            Vector3 forward = transform.forward;
            float halfAngle = viewAngle * 0.5f;
            Quaternion leftRot = Quaternion.Euler(0, -halfAngle, 0);
            Quaternion rightRot = Quaternion.Euler(0, halfAngle, 0);
            Vector3 leftDir = leftRot * forward;
            Vector3 rightDir = rightRot * forward;
            Gizmos.DrawLine(center, center + leftDir * viewDistance);
            Gizmos.DrawLine(center, center + rightDir * viewDistance);
        }
    }

    private void DrawNoiseGizmos()
    {
        if (_noiseRadiusGizmos > 0f)
        {
            Gizmos.color = Color.yellow;
            DrawCircleXZ(_noisePosGizmos, _noiseRadiusGizmos, 64);
        }
    }

    private void DrawCircleXZ(Vector3 center, float radius, int segments)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(radius, 0, 0);
        for (int i = 1; i <= segments; i++)
        {
            float rad = angleStep * i * Mathf.Deg2Rad;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
#endif
    #endregion
}

#region Interface for states
public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}
#endregion