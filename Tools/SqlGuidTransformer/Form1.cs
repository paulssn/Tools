namespace SqlGuidTransformer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			//we want no window to be visible
			Visible = false;
			ShowInTaskbar = false;
			base.OnLoad(e);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Transform();
		}

		private void Transform()
		{
			try
			{
				InitLogging();

				if (!Clipboard.ContainsText())
				{
					//TODO log
				}
				else
				{
					var inputText = Clipboard.GetText();
					var lines = inputText.Split("\n").Select(s => s.Trim()).ToList();
					if (!lines.All(LooksLikeGuid))
					{
						//todo log
						return;
					}

					var result = string.Join($",{Environment.NewLine}", lines.Select(s => $"'{s}'"));

					result = lines.Count > 1 ? $"in ({result})" : $"= {result}";

					Clipboard.SetText(result);
				}
			}
			catch (Exception e)
			{
				//log.Error("Unexpected Error", e); // TODO
				MessageBox.Show($"{e.GetType()}\r\nMessage: {e.Message}");
			}
			finally
			{
				Application.Exit();
			}
		}

		private void InitLogging()
		{
			// todo implement
		}

		private static bool LooksLikeGuid(string s)
		{
			// for now the length of a guid in the format 92d99a35-4b4c-475c-9e26-cd06da3dcf69
			return s.Length == 36; 
		}
	}
}
