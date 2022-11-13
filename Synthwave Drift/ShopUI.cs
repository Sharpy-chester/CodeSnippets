using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

	private CarSpawner _carSpawner;
	private GameInititaliser _gameInit;

	[SerializeField] private TextMeshProUGUI _carNameText;

	[SerializeField] private TextMeshProUGUI _currentCassettes;
	[SerializeField] private TextMeshProUGUI _cassettesToBuy;


	[SerializeField] private GameObject _buyButton;
	[SerializeField] private GameObject _playButton;
	[SerializeField] private GameObject _lockedText;



	private void Start()
	{
		_carNameText.text = _carSpawner.SelectedCar.CarName.ToUpper();
	}

	private void Awake()
	{
		_gameInit = FindObjectOfType<GameInititaliser>();
		_carSpawner = FindObjectOfType<CarSpawner>();
	}

	private void Update()
	{
		_currentCassettes.text = PlayerStats.CurrentCassettes.ToString();
		_cassettesToBuy.text = _carSpawner.SelectedCar.CassettesNeededToPurchase.ToString();
		CheckCarUnlockStatus();

	}

	private void CheckCarUnlockStatus()
	{
		switch (_carSpawner.SelectedCar.CurrentUnlockState)
		{
			case Car.UnlockState.Playable:
				_playButton.SetActive(true);
				_buyButton.SetActive(false);
				_lockedText.SetActive(false);
				break;

			case Car.UnlockState.Purchasable:
				_playButton.SetActive(false);
				_buyButton.SetActive(true);
				_lockedText.SetActive(false);
				break;

			case Car.UnlockState.Locked:
				_buyButton.SetActive(false);
				_playButton.SetActive(false);
				_lockedText.SetActive(true);

				break;
			default:
				break;
		}
	}

	public void CycleRight()
	{
		_carSpawner.LoadNextCar();
		_carNameText.text = _carSpawner.SelectedCar.CarName.ToUpper();
	}

	public void CycleLeft()
	{
		_carSpawner.LoadPreviousCar();
		_carNameText.text = _carSpawner.SelectedCar.CarName.ToUpper();
	}

	public void PlayGame()
	{
		this.gameObject.SetActive(false);
		_gameInit.StartGame();			

	}



	public void BuyCar()
	{
		PlayerStats.SpendCassetes(_carSpawner.SelectedCar.CassettesNeededToPurchase);	


	}

}
