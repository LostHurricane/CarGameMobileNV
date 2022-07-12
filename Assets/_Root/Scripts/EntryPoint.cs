using Profile;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

using Services.Analytics;


internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Start()
    {
        InitializeAsync();

        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);

        _mainController = new MainController(_placeForUi, profilePlayer);

       
    }

    async void InitializeAsync()
    {
        await UnityServices.InitializeAsync();
        List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
    }

    private void OnDestroy()
    {

        _mainController.Dispose();
    }

}
