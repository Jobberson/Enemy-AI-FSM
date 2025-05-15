using UnityEngine;
using Snog.EnemyFSM.Core;
using Snog.EnemyFSM.Configs;
using Snog.EnemyFSM.Interfaces;
using Snog.EnemyFSM.Enemy.States;

namespace Snog.EnemyFSM.Enemy
{
    /// <summary>
    /// MonoBehaviour that manages the state machine for the enemy.
    /// Initializes state context and handles transitions between states.
    /// </summary>
    public class EnemyStateMachine : MonoBehaviour
    {
        #region Inspector Fields
        [Header("General References")]
        [SerializeField] private Transform player;
        [SerializeField][Tooltip("Shared enemy config")] private EnemyConfig enemyConfig;
        [SerializeField][Tooltip("Specific config for wander state")] private WanderConfig wanderConfig;
        [SerializeField][Tooltip("Specific config for chase state")] private ChaseConfig chaseConfig;
        [SerializeField][Tooltip("Specific config for investigate state")] private InvestigateConfig investigateConfig;
        [SerializeField][Tooltip("Specific config for search state")] private SearchConfig searchConfig;
        [SerializeField][Tooltip("Specific config for recover state")] private RecoverConfig recoverConfig;
        [SerializeField][Tooltip("Specific config for stalk state")] private StalkConfig stalkConfig;
        [SerializeField][Tooltip("Movement controller implementing IMovementController")] private MonoBehaviour movementController; // cast to interface
        [SerializeField][Tooltip("Vision Sensor implementing IVisionDetector")] private EnemyVision vision;
        [SerializeField][Tooltip("Noise sensor implementing INoiseDetector")] private EnemyNoiseDetection noise;

        // private variables
        private StateMachne<IState> _fsm;
        private StateContext _context;
        private IState _wanderState;
        private IState _chaseState;
        private IState _investigateState;
        private IState _searchState;
        private IState _recoverState;
        private IState _stalkState;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            InitializeContext();
            InitializeStates();
            InitializeStateMachine();
        }

        private void Update()
        {
            _fsm.Tick();
        }
        #endregion

        #region Initialization
        private void InitializeContext()
        {
            _context = new StateContext
            {
                Owner       = transform,
                Player      = player,
                EnemyConfig = enemyConfig,
                WanderConfig    = wanderConfig,
                ChaseConfig     = chaseConfig,
                InvestigateConfig = investigateConfig,
                SearchConfig    = searchConfig,
                RecoverConfig   = recoverConfig,
                StalkConfig     = stalkConfig,
                Movement    = movementController as IMovementController,
                Vision      = vision as IVisionDetector,
                Noise       = noiseDetection as INoiseDetector
            };
        }

        private void InitializeStates()
        {
            _wanderState      = new WanderState(_context, _context.WanderConfig);
            _chaseState       = new ChaseState(_context, _context.ChaseConfig);
            _investigateState = new InvestigateState(_context, _context.InvestigateConfig);
            _searchState      = new SearchState(_context, _context.SearchConfig);
            _recoverState     = new RecoverState(_context, _context.RecoverConfig);
            _stalkState       = new StalkState(_context, _context.StalkConfig);
        }

        private void InitializeStateMachine()
        {
            _fsm = new StateMachine<IState>();
            ChangeState(_wanderState);
        }
        #endregion

        #region State Transition Helpers
        /// <summary>
        /// Switches to a new state, preserving the previous state in context.
        /// </summary>
        public void ChangeState(IState newState)
        {
            // Store old state
            _context.PreviousState = _fsm.CurrentState;

            // Transition
            _fsm.ChangeState(newState);
        }

        // Engage chase state.
        public void EngageChase() => ChangeState(_chaseState);

        // Return to wander state.
        public void ReturnToWander() => ChangeState(_wanderState);

        // Begin investigate state.
        public void InvestigateAt(Vector3 position)
        {
            _context.LastKnownPosition = position;
            ChangeState(_investigateState);
        }

        // Begin search state.
        public void BeginSearch() => ChangeState(_searchState);

        // Begin recover state.
        public void BeginRecovery() => ChangeState(_recoverState);

        // Begin stalk state.
        public void BeginStalk() => ChangeState(_stalkState);
        #endregion
    }
}
