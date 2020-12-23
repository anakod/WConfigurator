using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WConfigurator.Data;

namespace WConfigurator.Services
{
    public class InfraRemote
    {
        WatchService watch;
        List<InfraButton> buttons = null;

        public IEnumerable<InfraButton> Buttons => buttons;
        public ResponseButtonEdit CurrentButton { get; private set; }

        public InfraRemote(WatchService watchService)
        {
            watch = watchService;
            watch.AddHandler<ResponseButtonsList>("ir", new[] { "list", "add", "del" }, OnListButtons);
            watch.AddHandler<ResponseButtonEdit>("ir", new[] { "edit", "save"}, OnEditButton);
            watch.OnBeforeCommand += WatchOnBeforeCommand;
        }

        private void WatchOnBeforeCommand()
        {
            // Reset previous state
            buttons = null;
            CurrentButton = null;
        }

        public async Task ExecuteShowList()
        {
            await watch.ExecuteCommand(new InfraRequest("list"));
        }
        public async Task ExecuteAddNewButton(string newBtnName)
        {
            newBtnName = newBtnName.Replace(" ", "");
            await watch.ExecuteCommand(new InfraRequestValue("add", newBtnName));
        }
        public async Task ExecuteDeleteButton(string name)
        {
            await watch.ExecuteCommand(new InfraRequestValue("del", name));
        }
        public async Task ExecuteShowEditButton(string name)
        {
            await watch.ExecuteCommand(new InfraRequestValue("edit", name));
        }
        public async Task ExecuteSaveEditButton(ResponseButtonEdit buttonEdit)
        {
            buttonEdit.Type = "req";
            buttonEdit.Application = "ir";
            buttonEdit.Command = "save";
            if (string.IsNullOrWhiteSpace(buttonEdit.NewButtonName) || buttonEdit.NewButtonName == buttonEdit.ButtonName)
                buttonEdit.NewButtonName = null;

            if (buttonEdit.Mode != InfraButtonMode.RAW)
            {
                buttonEdit.Raw = null; // To reduce transmission size
                if (buttonEdit.HexCode.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    buttonEdit.HexCode = buttonEdit.HexCode.Substring(2);
            }

            await watch.ExecuteCommand(buttonEdit);
        }

        bool OnListButtons(ResponseButtonsList response)
        {
            buttons = response.Items.Select(x => new InfraButton(x)).ToList();
            return true;
        }

        bool OnEditButton(ResponseButtonEdit response)
        {
            response.HexCode = response?.HexCode?.ToUpper() ?? string.Empty;
            response.NewButtonName = response?.ButtonName ?? string.Empty;
            CurrentButton = response;
            return true;
        }
    }

    public class InfraButton
    {
        public string Name { get; private set; }

        public InfraButton(string name)
        {
            this.Name = name;
        }
    }
}
