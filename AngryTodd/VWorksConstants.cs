using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VWORKS_CONSTANTS
{
    namespace COMMAND
    {
        public class COMPILER
        {
            public static readonly int NONE = 0;
            public static readonly int DISALLOW_SEALED_PLATES = 1;
            public static readonly int DISALLOW_UNSEALED_PLATES = 2;
            public static readonly int SEALS_PLATE = 4;
            public static readonly int UNSEALS_PLATE = 8;
            public static readonly int DISALLOW_LIDDED_PLATES = 16;
            public static readonly int DISALLOW_UNLIDDED_PLATES = 32;
            public static readonly int LIDS_PLATE = 64;
            public static readonly int UNLIDS_PLATE = 128;
            public static readonly int ALL = (int)UInt16.MaxValue;
        }

        public class EDITOR
        {
            public static readonly int NONE = 0;
            public static readonly int HIDDEN = 1;
            public static readonly int PRIMARY = 2;
            public static readonly int SECONDARY = 4;
            public static readonly int PREPOST = 8;
            public static readonly int OMNIPRESENT = 16;
            public static readonly int ALL = (int)UInt16.MaxValue;
        }

        public class TASKREQUIRESLOCATION
        {
            public static readonly int NOT_REQUIRED = 0;
            public static readonly int REQUIRED = 1;
        }
    }

    namespace PARAMETER
    {
        public class SCRIPTABLE
        {
            /*The Scriptable attribute indicates whether a Script Variable dialog box
            opens when the user selects the parameter in the Task Parameters area and
            then presses the = (equals) key.
            Possible values:
            0 = The Script Variable dialog box does not open
            1 = The Script Variable dialog box opens
            The Scriptable attribute is only used for parameters where the value of the
            Type attribute is 19 or 30.
            Required: No
            Default value: 0 */
            public static readonly int SHOW_JSWIN = 0;
            public static readonly int NO_JSWIN = 1;
        }
        public class STYLE
        {
            /*The Style attribute represents how the parameter is rendered in the Task
            Parameters area.
            Possible values:
            0 = The parameter is always displayed in the Task Parameters area and is
            read-write
            1 = The parameter is always displayed in the Task Parameters area and is
            read- only
            2 = The parameter is hidden if the user selects the Hide disabled parameters
            check box in the Options dialog box; otherwise, the parameter is always
            displayed in the Task Parameters area and is read- only
            Required: No
            Default value: 0 */
            public static readonly int RW_SHOW = 0;
            public static readonly int R_SHOW = 1;
            public static readonly int DISABLED = 2;
        }

        public class TYPE
        {
            /*The Type attribute represents the type of the field in the Task Parameters
            area.
            Possible values:
            0 = Provides a Boolean check box.
            1 = Allows the user to specify a character string.
            2 = Provides a drop- down list box.
            3 = Provides a drop- down combo box.
            4 = Allows the user to specify a device location.
            5 = Allows the user to specify a labware or a fixed location. If the user
            specifies a labware, VWorks software selects the location.
            6 = Allows the user to specify both a location and the labware to use. VWorks
            software then passes the location to the plugin.
            7 = Opens the Well Selection dialog box.
            8 = Allows the user to specify an integer.
            9 = Allows the user to specify a file path.
            10 = Provides a labware drop- down list box.
            11 = Provides a liquid- class drop- down list box.
            12 = Allows the user to specify a decimal fraction.
            13 = Allows the user to specify a file path, where the value can be empty.
            14 = Allows the user to enter a password and displays a series of asterisks to
            hide the password string.
            15 = Allows the user to specify an IP address.
            16 = Allows the user to select a directory.
            17 = Allows the user to enter a time in the format hh:mm:ss.
            18 = Refers to an object in the JavaScript scripting context.
            19 = Allows the user to enter a date. The format depends on the region and
            language settings.
            20 = Allows the user to enter character strings that can wrap onto multiple
            lines.
            21 = Opens the Pipette Technique Editor.
            22 = Opens the Head Mode Selector dialog box.
            23 = Describes the tip positions of a tip box.
            24 = Opens the Field Composer dialog box.
            25 = Displays the available hit pick format files. For example, when the user
            clicks the down arrow in the Format file field of the Hit pick replicate task,
            the list that is displayed is of this type.
            26 = Deprecated. Used to show the available analog input names in the device
            file where the plugin resides.
            27 = Deprecated. Used to show the available digital input names in the device
            file where the plugin resides.
            28 = Deprecated. Used to show the available digital output names in the
            device file where the plugin resides.
            29 = Converts a parameter of this type to, and accesses it as, a JavaScript
            array object.
            30 = Allows the user to specify a duration in the format n Days hh:mm:ss.
            31 = Displays a multi- line text box.
            32 = Opens the color palette that enables the user to change the colors of
            various dialog box components.
            Required: Yes
             */
            public static readonly int BOOL_CHKBOX = 0;
            public static readonly int SPEC_TEXT = 1;
            public static readonly int DROP_LIST = 2;
            public static readonly int DROP_COMBO = 3;
            public static readonly int SPEC_LOCATION = 4;
            public static readonly int SPEC_LABWARE_OR_LOC = 5;
            public static readonly int SPEC_LABWARE_AND_LOC = 6;
            public static readonly int OPEN_WELL_SELECTION = 7;
            public static readonly int SPEC_INT = 8;
            public static readonly int SPEC_PATH = 9;
            public static readonly int LABWARE_DROP_LIST = 10;
            public static readonly int LIQCLASS_DROP_LIST = 11;
            public static readonly int SPEC_DEC = 12;
            public static readonly int SPEC_PATH_NULLABLE = 13;
            public static readonly int PASSWORD_FIELD = 14;
            public static readonly int IP_FIELD = 15;
            public static readonly int SELECT_DIR = 16;
            public static readonly int ENTER_TIME = 17;
            public static readonly int JS_OBJECT_REF = 18;
            public static readonly int ENTER_DATE = 19;
            public static readonly int STRING_WRAP_ENABLED = 20;
            public static readonly int OPEN_PIPETTE_EDITOR = 21;
            public static readonly int OPEN_HEADMODE_SELECTOR = 22;
            public static readonly int TIP_POSITIONS_DESC = 23;
            public static readonly int OPEN_FIELD_COMPOSER = 24;
            public static readonly int DISPLAY_HITPICK_FMT = 25;
            public static readonly int DEPRECATED_1 = 26;
            public static readonly int DEPRECATED_2 = 27;
            public static readonly int DEPRECATED_3 = 28;
            public static readonly int CONVERT_TO_JS_ARRAY = 29;
            public static readonly int SPEC_DURATION = 30;
            public static readonly int MULTILINE_TEXT = 31;
            public static readonly int COLOR_PALETTE_BOX = 32;
        }
    }
    namespace DEVICE
    {
        public class DYNAMICLOCATIONS
        {
            /*Currently, VWorks software does not use the DynamicLocations attribute.
            Indicates whether the device requires dynamic locations.
            Possible values:
            0 = The device does not require dynamic locations
            1 = The device requires dynamic locations
            Required: No
            Default value: 0
             */
            public static readonly int NOT_REQUIRED = 0;
            public static readonly int REQUIRED = 1;
        }

        public class BCREADER
        {
            /*The HasBarcodeReader attribute indicates whether the device has a barcode
            reader.
            Possible values:
            0 = The device does not have a barcode reader
            1 = The device has a barcode reader
            Required: No
            Default value: 1
             */
            public static readonly int NO_READER = 0;
            public static readonly int HAS_READER = 1;
        }

        public class MISCATTRIBUITES
        {
            /*The MiscAttributes attribute contains a bit field that is reserved for future
            functionality.
            Possible values:
            0 = No miscellaneous attributes exist for this device
            1 = Allow only combined pick- and- place robot moves; do not allow pick- uponly
            or place- only moves
            2 = Generate a compile- time warning if the location of an upstack or
            downstack process has not been scanned
            4 = The device needs a secondary teachpoint
            8 = Allow VWorks software to check if the stack is empty after a pick (used
            for devices that do not downstack until the move following the Downstack
            task)
            16 = Used for devices that can release stacks
            Required: No
            Default value: 0
             */
            public static readonly int NONE = 0;
            public static readonly int COMB_PICK_PLACE = 1;
            public static readonly int UPSTACK_WARNING = 2;
            public static readonly int SECONDARY_TEACHPOINT = 3;
            public static readonly int CK_STACK_EMPTY = 4;
            public static readonly int RELEASE_STACKS = 5;
        }

        public class PREFERREDTAB
        {
            /*The PreferredTab attribute contains an option in the Navigation Pane
            associated with the type of task that the device performs. If the device has
            more than one task, the PreferredTab attribute can be used to specify a
            different option for each task.
             */
            public static readonly string IO_DEVICE_HANDLING = "IO Device Handling";
            public static readonly string PLATE_HANDLING = "Plate Handling";
            public static readonly string PLATE_STORAGE = "Plate Storage";
            public static readonly string LIQUID_HANDLING = "Liquid Handling";
            public static readonly string READING = "Reading";
            public static readonly string OTHER = "Other";

        }
    }

    namespace LOCATION
    {
        public class GROUP
        {
            /*The Group attribute is a bitmask that defines a location grouping for this
            device.
            Grouping creates mutually exclusive locations on a device, that is, only one
            labware can be at a location in the group at a time. To enable this behavior,
            the Group attribute must be set to a value other than 0.
            To determine which value to use, do a bitwise inclusive OR operation on all
            the groups to which the location should belong. For example, if a location
            belongs to group 1 and group 2, the value to use is 3. Required: No, Default value: 0  */
            public static readonly int NOT_EXCLUSIVE = 0;
            public static readonly int GROUP1 = 1;
            public static readonly int GROUP2 = 2;
            public static readonly int GROUP3 = 3;
            public static readonly int GROUP4 = 4;
            public static readonly int GROUP5 = 5;
            public static readonly int GROUP6 = 6;
            public static readonly int GROUP7 = 7;
            public static readonly int GROUP8 = 8;
            public static readonly int GROUP9 = 9;
            public static readonly int GROUP10 = 10;
            public static readonly int ALL = (int)UInt16.MaxValue;
        }

        public class TYPE
        {
            /* The Type attribute is a bitmask that represents the type of access for the
            location.
            To determine the value to use, do a bitwise inclusive OR operation on all the
            access types for the location. Required: No, Default value: 1 */
            public static readonly int NO_LABWARE = 0;
            public static readonly int LABWARE_ALLOWED = 1;
            public static readonly int STACKING_ALLOWED = 2;
            public static readonly int SYSTEM_INOUT_ALLOWED = 4;
            public static readonly int INCUBATION_ALLOWED = 8;
            public static readonly int DELID_RELID_ALLOWED = 16;
            public static readonly int SYSTEM_IN_ALLOWED = 32;
            public static readonly int SYSTEM_OUT_ALLOWED = 64;
            public static readonly int CAN_BE_WASTEBIN = 128;
            public static readonly int MOUNTED_ALLOWED = 256;
            public static readonly int STATIC_ALLOWED = 512;
            public static readonly int CENTRIFUGE_ONLY = 1024;
            public static readonly int ALL = (int)UInt16.MaxValue;

        }

    }
}

