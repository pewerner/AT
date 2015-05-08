// Plugin Template for Agilent VWorks
// Author: Chance Elliott, 
// Company: Amyris
// Date: Jan 11, 2012
// Contact: elliott@amyris.com


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using System.Net.Mail;
using System.Collections.Generic;


namespace AngryTodd
{
    public class XMLParser
    {

        #region VWorks Template Code: DO NOT EDIT
        private string declarationString = "<?xml version='1.0' encoding='ASCII' ?>";

        public XMLParser()
        {
        }

        public string GetCommandName(string commandXML)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(commandXML);
                XmlNode commandNode = doc.SelectSingleNode("//Command");
                return commandNode.Attributes.GetNamedItem("Name").InnerText;
            }
            catch (Exception exp)
            {
                throw new Exception("Could not get CommandName from the command XML string. Exp: " + exp.Message);
            }
        }

        public string GetCommandAttribute(string commandXML, string Attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(commandXML);
                XmlNode commandNode = doc.SelectSingleNode("//Command");
                return commandNode.Attributes.GetNamedItem(Attribute).InnerText;
            }
            catch (Exception exp)
            {
                throw new Exception("Could not get Command Attribute from the command XML string. Exp: " + exp.Message);
            }
        }

        public string GetParameterValue(string commandXML, string ParameterName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(commandXML);
                XmlNode commandNode = doc.SelectSingleNode("//Parameter[@Name='" + ParameterName + "']");
                return commandNode.Attributes.GetNamedItem("Value").InnerText;
            }
            catch (Exception exp)
            {
                throw new Exception("Could not get the parameter value for " + ParameterName + " from the command XML string. " + exp.Message);
            }
        }

        public string GetAllMetaDataString()
        {
            XmlDocument _doc = new XmlDocument();
            XmlNode rootNode = BuildRootNode(_doc);
            _doc.CreateXmlDeclaration("1.0", "ASCII", "yes");
            BuildDeviceNode(rootNode);
            BuildVersionsNode(rootNode);
            BuildCommandsNode(rootNode);
            return declarationString + _doc.OuterXml;
        }

        public string GetDeviceMetaDataString()
        {
            XmlDocument _doc = new XmlDocument();
            XmlNode rootNode = BuildRootNode(_doc);
            BuildDeviceNode(rootNode);
            return declarationString + _doc.OuterXml;
        }

        public string GetCommandsMetaDataString()
        {
            XmlDocument _doc = new XmlDocument();
            XmlNode rootNode = BuildRootNode(_doc);
            BuildCommandsNode(rootNode);
            return declarationString + _doc.OuterXml;
        }

        public string GetVersionsMetaDataString()
        {
            XmlDocument _doc = new XmlDocument();
            XmlNode rootNode = BuildRootNode(_doc);
            BuildVersionsNode(rootNode);
            return declarationString + _doc.OuterXml;
        }

        private XmlNode BuildRootNode(XmlDocument doc)
        {
            XmlElement v11Element = doc.CreateElement("Velocity11");
            v11Element.SetAttribute("file", "MetaData");
            v11Element.SetAttribute("version", "1.0");
            XmlNode velocity11Node = doc.AppendChild(v11Element);
            XmlElement metadataElement = doc.CreateElement("MetaData");
            XmlNode metadataNode = velocity11Node.AppendChild(metadataElement);
            return metadataNode;
        }

        public string BuildCompilerErrorXML(List<string> CompilerMessages)
        {
            XmlDocument _doc = new XmlDocument();
            XmlNode rootNode = BuildRootNode(_doc);
            XmlNode compilersNode = _doc.CreateElement("CompilerErrors");
            foreach (string compilerMessage in CompilerMessages)
            {
                XmlElement compilerElement = _doc.CreateElement("CompilerError");
                string[] messageComponents = compilerMessage.Split(':');
                string errorType = "1";
                if (messageComponents[0] == "ERROR") errorType = "0";
                compilerElement.SetAttribute("Value", messageComponents[1]);
                compilerElement.SetAttribute("ErrorType", errorType);
                compilersNode.AppendChild(compilerElement);
            }
            rootNode.AppendChild(compilersNode);
            return declarationString + _doc.OuterXml;
        }
        #endregion


        #region Device-specific Meta Data


        private XmlNode BuildVersionsNode(XmlNode parentNode)
        {
            // Versions node
            XmlElement versionsElement = parentNode.OwnerDocument.CreateElement("Versions");
            XmlNode versionsNode = parentNode.AppendChild(versionsElement);
            XmlElement versionElement = parentNode.OwnerDocument.CreateElement("Version");
            XmlNode versionNode = versionsNode.AppendChild(versionElement);
            versionElement.SetAttribute("Author", "Chance Elliott");
            versionElement.SetAttribute("Company", "Amyris Inc");
            versionElement.SetAttribute("Date", "4/06/2012");
            versionElement.SetAttribute("Version", "1.1.0");
            return versionsNode;
        }

        private XmlNode BuildDeviceNode(XmlNode parentNode)
        {

            //DEVICE NODE
            XmlElement deviceElement = parentNode.OwnerDocument.CreateElement("Device");
            XmlNode deviceNode = parentNode.AppendChild(deviceElement);
            deviceElement.SetAttribute("Description", "Angry Flippin Todd"); // description of device - required.
            deviceElement.SetAttribute("DynamicLocations", VWORKS_CONSTANTS.DEVICE.DYNAMICLOCATIONS.NOT_REQUIRED.ToString()); // Not used currently in VWorks (For future functionality) - not required.
            deviceElement.SetAttribute("HardwareManufacturer", "TODO: My Manufacturer"); // Device manufacturer - not required
            deviceElement.SetAttribute("HasBarcodeReader", VWORKS_CONSTANTS.DEVICE.BCREADER.NO_READER.ToString()); // inicates weather or not device has built in BC Reader - not required.
            deviceElement.SetAttribute("MiscAttributes", VWORKS_CONSTANTS.DEVICE.MISCATTRIBUITES.NONE.ToString()); // reserved for future finctionality - not required.
            deviceElement.SetAttribute("Name", "TODO Device name"); // name of device - required
            deviceElement.SetAttribute("PreferredTab", VWORKS_CONSTANTS.DEVICE.PREFERREDTAB.PLATE_STORAGE.ToString()); // sets the default display tab for device fucntionality: IO Device Handling, 
            //Plate Handling, Plate Storage, Liquid Handling, Reading, Other. Not required.

            // DEVICE PARAMETER ELEMENTS
            XmlElement parametersElement = parentNode.OwnerDocument.CreateElement("Parameters");
            XmlNode parametersNode = deviceNode.AppendChild(parametersElement);

            // BEGIN DEVICE PARAMETER #1
            XmlElement parameterElement = parentNode.OwnerDocument.CreateElement("Parameter");
            parameterElement.SetAttribute("Name", "TODO: MY PARAMETER #1"); // name of parameter
            parameterElement.SetAttribute("Style", VWORKS_CONSTANTS.PARAMETER.STYLE.R_SHOW.ToString());  // determines wheather parameter is, read/write, read only or hidden   
            parameterElement.SetAttribute("Type", VWORKS_CONSTANTS.PARAMETER.TYPE.SPEC_INT.ToString()); // detrimines type of the field in the Task Parameters area (checkbox, dropdown, integer, filepath etc.)

            //// BEGIN COMPORT RANGE EXAMPLE:
            //// Creates a set of range values for the parameter above to display system comports. Used in conjuction with 
            //// the DROP_LIST parameter type, the user will be able to select a com port from a drop down list (rather than entering manually into a string field)
            //XmlElement rangesElement = parentNode.OwnerDocument.CreateElement("Ranges");
            //try
            //{
            //    string[] SerialPortNames = System.IO.Ports.SerialPort.GetPortNames(); ;
            //    foreach (string serialPort in SerialPortNames)
            //    {
            //        XmlElement rangeElement = parentNode.OwnerDocument.CreateElement("Range");
            //        rangeElement.SetAttribute("Value", serialPort);
            //        rangesElement.AppendChild(rangeElement);
            //    }
            //}
            //catch
            //{
            //    //?
            //}
            //parameterElement.AppendChild(rangesElement);
            //// END comport range example


            //// BEGIN UPPER/LOWER LIMIT RANGE EXAMPLE:
            //// Creates a lower and upper bound range for a given parameter. User will only be able to enter a value between 
            //// the 2 ranges
            //XmlElement rangesElement = parentNode.OwnerDocument.CreateElement("Ranges"); // create parent ranges node
            //XmlElement rangeElement = parentNode.OwnerDocument.CreateElement("Range"); // create minimum range element
            //rangeElement.SetAttribute("Value", "1"); // set minimum value
            //rangesElement.AppendChild(rangeElement); // add minimum range element to ranges node
            //rangeElement = parentNode.OwnerDocument.CreateElement("Range"); //create max range element
            //rangeElement.SetAttribute("Value", "3000"); // set max value
            //rangesElement.AppendChild(rangeElement); // add max range element to ranges node
            //parameterElement.AppendChild(rangesElement); // add ranges node to parameter element node
            //// END UPPER/LOWER LIMIT RANGE EXAMPLE

            parametersNode.AppendChild(parameterElement); // add parameter to parameters
            //// END DEVICE PARAMETER #1

            // Location Attribute (Device Nest) - Can be omitted if device has no nests (as is AutoFill, EPC controller... etc)
            XmlElement locationsElement = parentNode.OwnerDocument.CreateElement("Locations");
            XmlNode locationsNode = deviceNode.AppendChild(locationsElement);
            XmlElement locationElement = parentNode.OwnerDocument.CreateElement("Location");
            locationsNode.AppendChild(locationElement);
            locationElement.SetAttribute("Group", VWORKS_CONSTANTS.LOCATION.GROUP.NOT_EXCLUSIVE.ToString()); // bitmask that defines a location grouping for this device. Default value 0.
            locationElement.SetAttribute("Type", (VWORKS_CONSTANTS.LOCATION.TYPE.LABWARE_ALLOWED | // bitmask that represents the type of access for the location.
                                                    VWORKS_CONSTANTS.LOCATION.TYPE.LABWARE_ALLOWED).ToString()); // in this example, labware can be moved to location, and incubated at location.
            locationElement.SetAttribute("Name", "AT's Crib"); // Name of nest



            XmlElement dimensionsElement = parentNode.OwnerDocument.CreateElement("StorageDimension");
            XmlNode dimensionsNode = deviceNode.AppendChild(dimensionsElement);
            //XmlElement dimensionElement = parentNode.OwnerDocument.CreateElement("StorageDimension");

            //dimensionsElement.AppendChild(dimensionElement);
            dimensionsElement.SetAttribute("DirectStorageAccess", "1"); // bitmask that defines a location grouping for this device. Default value 0.
            dimensionsElement.SetAttribute("Name0", "Test");
            dimensionsElement.SetAttribute("Name1", "Test2");
            dimensionsElement.SetAttribute("Size", "1");







            return deviceNode;
        }

        private XmlNode BuildCommandsNode(XmlNode parentNode)
        {
            string globalEditorAttribute = (VWORKS_CONSTANTS.COMMAND.EDITOR.OMNIPRESENT |
                                       VWORKS_CONSTANTS.COMMAND.EDITOR.PRIMARY |
                                       VWORKS_CONSTANTS.COMMAND.EDITOR.PREPOST
                                      ).ToString(); // if this value is not used, commands will not appear in the protocol editor window

            //Create the root of all commands:
            XmlElement commandsElement = parentNode.OwnerDocument.CreateElement("Commands");
            XmlNode commandsNode = parentNode.AppendChild(commandsElement); //this is returned after commands tree is built


            // BEGIN Command # 1 
            // Use this entire section as a template. If there is more than 1 command
            // cut and paste the entire section for the new command.

            // This command's root node>element
            XmlElement commandElement = parentNode.OwnerDocument.CreateElement("Command");
            {

                commandElement.SetAttribute("Compiler", VWORKS_CONSTANTS.COMMAND.COMPILER.NONE.ToString()); //designates any labware or labware state restrictions (0=none)
                commandElement.SetAttribute("Description", "TODO: Does Something"); //decription of command
                commandElement.SetAttribute("Editor", globalEditorAttribute); //command is available in the all areas of protocol editor window (pre,post,main, and sub-process)
                commandElement.SetAttribute("Name", "TODO"); //name of command
                commandElement.SetAttribute("TaskRequiresLocation", VWORKS_CONSTANTS.COMMAND.TASKREQUIRESLOCATION.REQUIRED.ToString()); //whether the task requires a location.
                commandElement.SetAttribute("PreferredTab", VWORKS_CONSTANTS.DEVICE.PREFERREDTAB.PLATE_HANDLING.ToString()); // what tab of the protocol editor to display task, 
                // you can override what was defined under the device node here if you so desire.

                // if command has paramter(s), create the paramters parent node>element
                XmlElement parametersElement = parentNode.OwnerDocument.CreateElement("Parameters");
                XmlNode parametersNode = commandElement.AppendChild(parametersElement);

                //// BEGIN EXAMPLE COMMAND PARAMETER ELEMENT
                //// This is a parameter of the above command, as in Shake Time, Seal Temperature, Dispense Volume etc. Delete if paramters are not used.
                // create parameter node
                XmlElement parameterElement = parentNode.OwnerDocument.CreateElement("Parameter");
                {
                    parameterElement.SetAttribute("Name", "TODO: Paramter Name"); // name of parameter
                    parameterElement.SetAttribute("Style", VWORKS_CONSTANTS.PARAMETER.STYLE.RW_SHOW.ToString()); // determines wheather parameter is, read/write, read only or hidden
                    parameterElement.SetAttribute("Type", VWORKS_CONSTANTS.PARAMETER.TYPE.ENTER_TIME.ToString());     // detrimines type of the field in the Task Parameters area (checkbox, dropdown, integer, filepath etc.)
                    parameterElement.SetAttribute("Units", "TODO: My Units");  // The Units attribute specifies the appropriate unit of measure for the parameter
                    parameterElement.SetAttribute("Description", "TODO: Description of Paramter"); // The description of the parameter
                    parameterElement.SetAttribute("Value", "1");    // default value to display

                    //// You could add range elements here in the exact same way they were added to the device paramters above
                    //// see the comport ranges list and the min/max ranges examples

                }
                parametersNode.AppendChild(parameterElement);
                //// END EXAMPLE COMMAND PARAMETER ELEMENT
            }


            commandsNode.AppendChild(commandElement); // add command element to commands parent node.
            // END Command # 1

            //////////////// ADD ALL COMMANDS ABOVE THIS LINE /////////////////
            ///////////////////////////////////////////////////////////////////
            return commandsNode;

        }

        #endregion

    }
    public class Utilities
    {

        //converts 34x34 images for VWorks use
        public static object ConvertToIPicture(Image image)
        {
            return ImageOLEConverter.Instance.ConvertToIPicture(image);
        }

        private class ImageOLEConverter : AxHost
        {
            public static readonly ImageOLEConverter Instance = new
            ImageOLEConverter();

            private ImageOLEConverter()
                : base(Guid.Empty.ToString())
            {
            }

            public object ConvertToIPicture(Image image)
            {
                return AxHost.GetIPictureFromPicture(image);
            }
        }

        // returns profile names if they are stored in the registry. CURRENTLY ONLY USED BY PLUGIN'S WRITTEN BY AGILENT
        public string[] GetProfilesRegisteredInRegistry(string DeviceProfileNode)
        {
            try
            {
                return Registry.LocalMachine.OpenSubKey(DeviceProfileNode).GetSubKeyNames();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // returns profile name and data to registry. CURRENTLY ONLY USED BY PLUGIN'S WRITTEN BY AGILENT
        public bool SaveProfileInRegistry(string DeviceProfileNode, string key, string value)
        {
            try
            {
                // open or create subkey nodes, set profile values.
                Registry.LocalMachine.CreateSubKey(DeviceProfileNode + "\\" + DeviceProfileNode).SetValue(key, value);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private string GetVWorksInstallationLocation()
        {
            string installerPath = String.Empty;
            string currentVersion = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Velocity11\\VWorks", "VWorks", String.Empty).ToString();
            if (!String.IsNullOrEmpty(currentVersion))
            {
                installerPath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Velocity11\\VWorks", "VWorks " + currentVersion + " Installation location", String.Empty).ToString();
            }
            return installerPath;
        }


        // simple input box
        public DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }



    }
}
