using Xamarin.Forms;

namespace StatusQueue.ControlsEx
{

    public class ClickableImage : Image
    {
        public static BindableProperty OnClickProperty =
            BindableProperty.Create("OnClick", typeof(Command), typeof(ClickableImage));

        public Command OnClick
        {
            get { return (Command)GetValue(OnClickProperty); }
            set { SetValue(OnClickProperty, value); }
        }

        public ClickableImage()
        {
            GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(DisTap) });
        }

        private void DisTap(object sender)
        {
            if (OnClick != null)
            {
                OnClick.Execute(sender);
            }
        }

    }
}
