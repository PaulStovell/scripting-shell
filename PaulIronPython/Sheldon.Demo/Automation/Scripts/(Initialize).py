import clr;

clr.AddReferenceByPartialName("WindowsBase");
clr.AddReferenceByPartialName("PresentationCore");
clr.AddReferenceByPartialName("PresentationFramework");
clr.AddReferenceByPartialName("IronPython");
clr.AddReferenceByPartialName("Sheldon");
clr.AddReferenceByPartialName("Sheldon.Demo");

from System import *;
from System.ComponentModel import *;
from System.IO import *;
from System.Media import *;
from System.Windows import *;
from System.Windows.Converters import *;
from System.Windows.Controls import *;
from System.Windows.Data import *;
from System.Windows.Markup import *;
from System.Windows.Media import *;

from Sheldon import *;
from Sheldon.Demo import *;
from Sheldon.Demo.Automation import *;
