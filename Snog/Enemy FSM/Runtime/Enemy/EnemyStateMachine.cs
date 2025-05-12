using UnityEngine;
using Snog.EnemyFSM.Core;
using Snog.EnemyFSM.Configs;
using Snog.EnemyFSM.Interfaces;
using Snog.EnemyFSM.Enemy.States;

namespace Snog.EnemyFSM.Enemy 
{
    /// <summary>
    /// MonoBevahiour that manages the state machine.
    /// Initializes state context and transitions between states.
    /// </summary>
    public public class EnemyStateMachine : MonoBehaviour
    {
        #region Variables
        [Header("General References")]
        [SerializeField] private Transform player;
        [SerializeField][Tooltip("Shared enemy config")] private EnemyConfig enemyConfig;
        [SerializeField][Tooltip("Speficic config for wander state")] private WanderConfig wanderConfig;
        [SerializeField][Tooltip("Speficic config for chase state")] private ChaseConfig chaseConfig;
        [SerializeField][Tooltip("Speficic config for investigate state")] private InvestigateConfig investigateConfig;
        [SerializeField][Tooltip("Speficic config for search state")] private SearchConfig searchConfig;
        [SerializeField][Tooltip("Speficic config for recover state")] private RecoverConfig recoverConfig;
        [SerializeField][Tooltip("Movement controller implementing IMovementController")] MonoBehaviour movementController; // cast to interface
        [SerializeField][Tooltip("Vision Sensor implementing IVisionDetector")] private EnemyVision vision;
        [SerializeField][Tooltip("Noise sensor implementing INoiseDetector")] private EnemyNoiseDetection noise;

        // private variables
        private Statemachne<IState> _fsm;
        private StateContect _context;
        private IState _wanderState;
        private IState _chaseState;
        private IState _investigateState;
        private IState _searchState;
        private IState _recoverState;
        #endregion

        #region Unity Methods
        private void Awake() 
        {
            InitializeContext();
            InitializeStates;
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
                Owner = this.Transform;
                Player = player;
                EnemyConfig = enemyConfig;
                Movement = movementController as IMovementController;
                Vision = vision;
                Noise = noise;
            };
        }

        private void InitializeStates()
        {
            _wanderState = new WanderState(_context, _wanderConfig);
            _chaseState = new ChaseState(_context, _chaseConfig);
            _investigateState = new InvestigateState(_context, _investigateConfig);
            _searchState = new SearchState(_context, _searchConfig);
            _recoverState = new RecoverState(_context, _recoverConfig);
        }
        #endregion
    } 
}