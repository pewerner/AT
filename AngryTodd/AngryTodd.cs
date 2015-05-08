// Instrument plugin for Agilent VWorks
// Author: Chance Elliott, 
// Company: Amyris
// Date: March 28, 2011
// Contact: elliott@amyris.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
// TODO: add low device control ref: using MyDevice.MyControllerClass

namespace AngryTodd
{
 // all VWorks Drivers must implement IWorksDriver.IWorksDriver, IWorksDriver.IWorksDiags and IWorksDriver.IControllerClient interfaces
    public class VWorksPlugin : IWorksDriver.IWorksDriver, IWorksDriver.IWorksDiags, IWorksDriver.IControllerClient, IWorksDriver.IStorageDriver
    {
        // TODO: Deivce controller bject ref - replace "Object" with the data type for 
        // your controller library: MyDevice.MyControllerClass _myControllerRef;
        Object _myControllerRef;

        // VWorks controller object
        IWorksDriver.CWorksController _Controller;
        //IWorksDriver.IStorageDriver _PlateStore = null;


        #region IWorksDriver Members
        // current VWorks command (Parsed)
        string _curCommand = String.Empty;

        // current VWorks command (Raw XML)
        string _curCommandXML = String.Empty;

        // current error message
        string _errorMessage = String.Empty;

        // class constructor
        public VWorksPlugin()
        {

            //_PlateStore.QueryStorageLocations("Stromg");
            // Constructor co de
            // TODO: 
            // Set obj ref: _myControllerRef = new MyDevice.MyControllerClass();
            // instantiate low level driver so you can check the driver version even 
            // if it's profile hasnt been initialized.

        }

        //VWorks software calls the Abort method to terminate a specific task or to
        //terminate all currently executing tasks. TODO: add device specific abort code.
        public void Abort(string SomeStringXML)
        {
            try
            {
                if (_myControllerRef != null)
                {
                    // TODO: call halt proceedure (if applicable)
                }
            }
            catch (Exception exp)
            {
                //TODO: handle error
                SetError("Some error.", exp);
            }
        }

        //VWorks software calls the Close method to tell the plugin to terminate the
        //connection to a device.
        //IMPORTANT The Close method is a synchronous blocking call. It should not
        //return until the device is completely closed. TODO: add device specific shutdown code.
        public void Close()
        {
            try
            {
                if (_myControllerRef != null)
                {
                    //TODO: Call shutdown proceedure if applicable

                    //destroy controller object refrence
                    _myControllerRef = null;
                }
            }
            catch (Exception exp)
            {
                //TODO: handle error
                SetError("Some error.", exp);
            }

        }

        //VWorks software calls the Command method to tell the plugin to execute the
        //specified task. TODO: add all commands the device can execute.
        public IWorksDriver.ReturnCode Command(string CommandXML)
        {
            // clear error message
            ClearError();
            // new XML parser obj
            XMLParser xmlParserRef = new XMLParser();

            // Extract the command name from the CommandXML string.
            _curCommand = xmlParserRef.GetCommandName(CommandXML);

            // set current raw XML command variable
            _curCommandXML = CommandXML;
            switch (_curCommand)
            {
                case "TODO":
                    try
                    {
                        //TODO: call  fuction;
                    }
                    catch (Exception exp)
                    {
                        //TODO: handle exception
                        SetError("Some Error", exp);

                        // return fail
                        return IWorksDriver.ReturnCode.RETURN_FAIL;
                    }
                    break;
            }

            // return success
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;



        }

        //VWorks software calls the Compile method to notify the plugin of the state of
        //a protocol’s compile sequence use this to validate instrument and command parameters 
        // prior to executing a protocol. TODO: handle compile time erros and warnings for your device
        public string Compile(IWorksDriver.CompileType iCompileType, string MetaDataXML)
        {
            string comPortName = string.Empty;


            string compilerResult = String.Empty;
            if (iCompileType == IWorksDriver.CompileType.COMPILE_TASK_POSTPROCESS ||
                iCompileType == IWorksDriver.CompileType.COMPILE_TASK_PREPROCESS ||
                iCompileType == IWorksDriver.CompileType.COMPILE_TASK_PROCESS ||
                iCompileType == IWorksDriver.CompileType.COMPILE_TASK_SUBPROCESS ||
                iCompileType == IWorksDriver.CompileType.COMPILE_BEGIN)
            {
                XMLParser xmlParserRef = new XMLParser();
                Utilities utilitiesRef = new Utilities();
                string commandName = xmlParserRef.GetCommandName(MetaDataXML);
                List<string> compilerMessageList = new List<string>();
                switch (commandName)
                {

                    // Called at start of Compilation.
                    case "Compile":
                        try
                        {
                            // example: check to see if this is a vaild comport
                            comPortName = xmlParserRef.GetParameterValue(MetaDataXML, "Com Port");

                            if (!ComPortExists(comPortName))
                            {
                                compilerMessageList.Add("ERROR:The value for the Com Port is invalid: [" + comPortName + "]");
                            }

                        }
                        catch
                        {
                            // couldn't read comport value
                            compilerMessageList.Add("ERROR:Could not retrieve the value for the Com Port");
                        }

                        // TODO - handle any other compile time checks - this will throw the warning s and errors that you see when you compile a protocol
                        break;
                }
                compilerResult = xmlParserRef.BuildCompilerErrorXML(compilerMessageList);
            }
            return compilerResult;
        }

        //VWorks software uses the ControllerQuery method in conjunction with the
        //IWorksController Query method to provide the means for two plugins to
        //communicate with each other. Not typically used. TEMPLATE DO NOT EDIT
        public string ControllerQuery(string Query)
        {
            return String.Empty;

        }

        //VWorks software calls the Get32x32Bitmap method to get a bitmap image
        //from the plugin that represents a device or the specified task. TODO: Create icons
        //for device tasks, add thier cases and paths to the code below.
        public stdole.IPictureDisp Get32x32Bitmap(string CommandName)
        {
            stdole.IPictureDisp pictureRef = null;
            switch (CommandName)
            {
                case "TODO":
                    // TODO add path to task image: a 32x32 8 bit bmp icon for the task
                    pictureRef = (stdole.IPictureDisp)Utilities.ConvertToIPicture(global::AngryTodd.Properties.Resources.AngryTodd);
                    break;
                default:
                    // Icon for the Device.
                    // TODO add path to device icon image: 
                    pictureRef = (stdole.IPictureDisp)Utilities.ConvertToIPicture(global::AngryTodd.Properties.Resources.AngryTodd);
                    break;
            }
            return pictureRef;
        }

        //VWorks software calls the GetDescription method to get the description for
        //the specified task from the plugin. TODO: Add cases and descriptions for your 
        //devices commands here.
        public string GetDescription(string CommandXML, bool Verbose)
        {
            XMLParser xmlParserRef = new XMLParser();
            string commandName = xmlParserRef.GetCommandName(CommandXML);
            string description = String.Empty;
            switch (commandName)
            {
                case "TODO":
                    description = "Need TODO something here";
                    break;
                default:
                    description = "Unknown command";
                    break;
            }

            return description;
        }

        //VWorks software calls the GetErrorInfo method when the plugin returns a
        //value other than RETURN_SUCCESS for a method with a retVal output
        //parameter of type ReturnCode. TEMPLATE DO NOT EDIT
        public string GetErrorInfo()
        {
            return _errorMessage;
        }

        //VWorks software calls the GetLayoutBitmap method to get a dynamic bitmap
        //from the plugin. The bitmap displays the specified labware and its location on
        //a device (like the deck of a bravo). TODO: return image of device
        public stdole.IPictureDisp GetLayoutBitmap(string LayoutInfoXML)
        {
            // TODO- path to device icon image
            return (stdole.IPictureDisp)Utilities.ConvertToIPicture(global::AngryTodd.Properties.Resources.AngryTodd);
        }

        //VWorks software calls the GetMetaData method for the following reasons:
        //• To get all metadata for a plugin
        //• To notify the plugin when the user changes device parameters and task
        //  parameters. TEMPLATE DO NOT EDIT.
        public string GetMetaData(IWorksDriver.MetaDataType iDataType, string current_metadata)
        {
            XMLParser parser = new XMLParser();
            string metaData = String.Empty;
            switch (iDataType)
            {
                case IWorksDriver.MetaDataType.METADATA_ALL:
                    metaData = parser.GetAllMetaDataString();
                    break;
                case IWorksDriver.MetaDataType.METADATA_COMMAND:
                    metaData = parser.GetCommandsMetaDataString();
                    //metaData = String.Empty;
                    break;
                case IWorksDriver.MetaDataType.METADATA_DEVICE:
                    metaData = parser.GetDeviceMetaDataString();
                    break;
                case IWorksDriver.MetaDataType.METADATA_VERSION:
                    metaData = parser.GetVersionsMetaDataString();
                    break;
            }
            return metaData;
          

            //throw new NotImplementedException();
        }

        //If the user clicks the Ignore and Continue… button, VWorks software calls
        //the Ignore method to tell the plugin to ignore the error. TODO: deal with anything
        //that will allow your device to ignore this error.
        public IWorksDriver.ReturnCode Ignore(string SomeStringXML)
        {
            _errorMessage = String.Empty;
            _curCommand = String.Empty;
            ClearError();
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        //VWorks software calls the Initialize method to tell the plugin to initialize
        //a device. TODO: handle device initialization here
        public IWorksDriver.ReturnCode Initialize(string CommandXML)
        {
            string comPort = string.Empty;
            XMLParser xmlParserRef = new XMLParser();

            // Decode the command name from the CommandXML string.
            _curCommandXML = CommandXML;
            _curCommand = xmlParserRef.GetCommandName(CommandXML);


            // create new object if empty
            if (_myControllerRef == null)
            {
                // TODO: Set obj ref: _myControllerRef = new MyDevice.MyControllerClass();
            }

            // get com port - this tag must match what was defined in the utilities class

            // TODO: catch any other pre init parameters that you can catch before initing device

            // init
            try
            {
                //TODO: init device
            }
            catch (Exception exp)
            {
                //TODO: Set error
                SetError("Some error", exp);

                // return fail
                return IWorksDriver.ReturnCode.RETURN_FAIL;
            }

            // return success, device initialized
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        //VWorks software calls the IsLocationAvailable method (repeatedly) during
        //task scheduling to ask the plugin whether the target location is available for a
        //labware- handling process. This method was created for complicated multi- robot systems to prevent one
        //robot from reaching into the envelope of another robot, which could cause a
        //robot crash.  TEMPLATE Not typically modified.
        public bool IsLocationAvailable(string LocationAvailableXML)
        {
            return true;
        }

        //VWorks software calls the MakeLocationAvailable method when a labware
        //is scheduled for delivery to a specified location. The plugin should perform all
        //actions necessary to ensure that the target location is available for a labwarehandling
        //process. For example, a device might need to open a door and extend
        //a labware stage at this point.TODO: open clamps, position nest etc to get ready 
        //for robot access
        public IWorksDriver.ReturnCode MakeLocationAvailable(string LocationAvailableXML)
        {
            // TODO: if there is anything mechanical that needs to be activated in order to access nests, do it here

            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        //VWorks software calls the PlateDroppedOff method to notify the plugin that
        //a labware was dropped off at the location specified in the previous call to the
        //MakeLocationAvailable method. TEMPLATE Not typically modified.
        public IWorksDriver.ReturnCode PlateDroppedOff(string PlateInfoXML)
        {
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        //VWorks software calls the PlatePickedUp method to notify the plugin that a
        //labware was picked up at the location specified in the previous call to the
        //MakeLocationAvailable method. TEMPLATE Not typically modified.
        public IWorksDriver.ReturnCode PlatePickedUp(string PlateInfoXML)
        {
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        //VWorks software calls the PlateTransferAborted method to notify the
        //plugin that a labware- transfer process was aborted. This is typically due to a
        //robot malfunction followed by user intervention, that is, when the user clicks
        //the Abort button in the standard error dialog box. TEMPLATE Not Typically modified.
        public void PlateTransferAborted(string PlateInfoXML)
        {
            // do nothing
        }

        //VWorks software calls the PrepareForRun method to notify the plugin that
        //the user clicked the Start button in the VWorks window. This method, which is
        //called each time a protocol is run, tells the plugin that a run is starting. If the
        //plugin maintains per- run state information, the state should be cleared during
        //this call. TEMPLATE not typically modified
        public IWorksDriver.ReturnCode PrepareForRun(string LocationInfoXML)
        {
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        //If the user clicks the Retry button, VWorks software calls the Retry
        //method to tell the plugin to retry the task. TEMPLATE, you shouldnt need to edit.
        public IWorksDriver.ReturnCode Retry(string SomeStringXML)
        {
            IWorksDriver.ReturnCode returnCode;
            try
            {
                if (!String.IsNullOrEmpty(_curCommand))
                {
                    if (_curCommand == "Initialize")
                        returnCode = this.Initialize(_curCommandXML);
                    else
                        returnCode = this.Command(_curCommandXML);
                }
                else
                {
                    Exception exp = new Exception("Empty XML");
                    SetError("Retry Failed, command was empty", exp);
                    returnCode = IWorksDriver.ReturnCode.RETURN_FAIL;
                }
                return returnCode;
            }
            catch (Exception exp)
            {
                SetError("Retry Failed", exp);
                return IWorksDriver.ReturnCode.RETURN_FAIL;
            };
        }
        #endregion

        #region IWorksDiags Members

        //VWorks software calls the ShowDiagsDialog method to tell the plugin to
        //open its diagnostics dialog box. This method displays both modal and modeless
        //dialog boxes. Do not implement in VW 4. TEMPLATE DO NOT EDIT.
        public void ShowDiagsDialog(IWorksDriver.SecurityLevel iSecurity)
        {
            //do nothing
        }

        //VWorks software calls the CloseDiagsDialog method to tell the plugin to
        //close its diagnostics dialog box. TEMPLATE DO NOT EDIT.
        public IWorksDriver.ReturnCode CloseDiagsDialog()
        {
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        //Obsolete. This method should be
        //implemented as return E_NOTIMPLMENTED
        public IWorksDriver.ReturnCode IsDiagsDialogOpen()
        {
            return IWorksDriver.ReturnCode.RETURN_FAIL;
        }

        //VWorks software calls the ShowDiagsDialog method to tell the plugin to
        //open its diagnostics dialog box. This method displays both modal and modeless
        //dialog boxes.
        public void ShowDiagsDialog(IWorksDriver.SecurityLevel iSecurity, bool bModal)
        {

            DiagnosticForm diagForm = new DiagnosticForm(_myControllerRef);
            diagForm.ShowDialog();
            _Controller.OnCloseDiagsDialog((IWorksDriver.CControllerClient)this);
        }
        #endregion

        #region




        #endregion



        #region IControllerClient Members
        // TEMPLATE DO NOT EDIT
        public void SetController(IWorksDriver.CWorksController Controller)
        {
            _Controller = Controller;
        }

        // TEMPLATE DO NOT EDIT
        void IWorksDriver.IControllerClient.SetController(IWorksDriver.CWorksController Controller)
        {
            _Controller = Controller;
        }
        #endregion

        #region OddsnEnds

        // TEMPLATE DO NOT EDIT
        private void SetError(string message, Exception exp)
        {
            _errorMessage = message + ". " + exp.Message;
        }

        // TEMPLATE DO NOT EDIT
        private void SetError(string message)
        {
            _errorMessage = message;
        }

        // TEMPLATE DO NOT EDIT
        private void ClearError()
        {
            _errorMessage = String.Empty;
        }

        // CHECKS FOR COMPORT EXISTENCE
        private bool ComPortExists(string port)
        {
            try
            {
                string[] SerialPortNames = System.IO.Ports.SerialPort.GetPortNames();
                return SerialPortNames.Contains(port);
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region

        IWorksDriver.ReturnCode IWorksDriver.IStorageDriver.GetStorageLocations(out string StorageLocationsXML)
        {
            //StorageLocationsXML = String.Empty;



             throw new NotImplementedException();
        }

        IWorksDriver.ReturnCode IWorksDriver.IStorageDriver.LoadPlate(string Labware, IWorksDriver.PlateFlagsType PlateFlags, string LoadPlateLocationXML)
        {
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        IWorksDriver.ReturnCode IWorksDriver.IStorageDriver.LookupLocations(bool Load, string Labware, IWorksDriver.PlateFlagsType PlateFlags, string StorageLocationXML, out string ExternalLocationsXML)
        {
            ExternalLocationsXML = @"<?xml version='1.0' encoding='ASCII' ?>
                                    <Velocity11 file='MetaData' md5sum='d9def80adb4895a149000be927986a3e' version='1.0' >
                                     <LocationVector >
                                     <Locations >
                                      <Location Group='0' MaxStackHeight='460' Name='Primary (Load/Unload) Pad'Offset='0' Type='4' />
                                     </Locations>
                                     </LocationVector>
                                     </Velocity11>";
            
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        IWorksDriver.ReturnCode IWorksDriver.IStorageDriver.QueryStorageLocations(string QueryXML)
        {
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        IWorksDriver.ReturnCode IWorksDriver.IStorageDriver.UnloadPlate(string Labware, IWorksDriver.PlateFlagsType PlateFlags, string UnloadPlateLocationXML)
        {
            return IWorksDriver.ReturnCode.RETURN_SUCCESS;
        }

        #endregion




    }


}


 