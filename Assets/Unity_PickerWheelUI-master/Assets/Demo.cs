using UnityEngine ;
using EasyUI.PickerWheelUI ;
using UnityEngine.UI ;

public class Demo : MonoBehaviour {
   [SerializeField] private Button uiSpinButton ;
   [SerializeField] private Text uiSpinButtonText ;

   [SerializeField] private PickerWheel pickerWheel ;


   private void Start () {
      uiSpinButton.onClick.AddListener (() => {

          Spin();

      }) ;

   }

    private void Spin()
    {
        uiSpinButton.interactable = false;
        uiSpinButtonText.text = "Spinning";

        pickerWheel.OnSpinEnd(wheelPiece => {
            uiSpinButton.interactable = true;
            uiSpinButtonText.text = "Spin";
        });

        pickerWheel.Spin();
    }

}
