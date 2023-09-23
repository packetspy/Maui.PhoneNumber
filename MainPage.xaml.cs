namespace Maui.PhoneNumber
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber = string.Empty;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTranslate_Clicked(object sender, EventArgs e)
        {
            string inputtedNumber = PhoneNumberText.Text;
            translatedNumber = PhoneWordTranslator.ToNumber(inputtedNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                //CallButton.Text = $"Call to {translatedNumber}";
                CallButton.Text = "Call " + translatedNumber;
            }
            else
            {
                CallButton.IsEnabled = false;
            }   CallButton.Text = "Call";
        }

        private async void OnCall_Clicked(object sender, EventArgs e)
        {
            if(await this.DisplayAlert(
                "Dial number",
                $"Would you like to call {translatedNumber} ?",
                "Yes", "No"
                ))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                        PhoneDialer.Default.Open(translatedNumber);
                }
                catch (ArgumentException) 
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "Ok");
                }
                catch (Exception)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing failed", "Ok");
                }
            }
        }
    }
}