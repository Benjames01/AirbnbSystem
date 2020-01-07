# The Assignment Scenario
## The Data
Since 2008, guests and hosts have used Airbnb to expand on traveling possibilities and present more unique, personalized way of experiencing the world. This dataset describes the listing activity and metrics in NYC, NY for 2019. This dataset has all the data you need to find out more about hosts, geographical availability and other necessary metrics to make predictions and draw
conclusions.

This data was sourced from https://www.kaggle.com/dgomonov/new-york-city-airbnbopen-data/downloads/new-york-city-airbnb-open-data.zip/3. Note it has been formatted specifically for your coursework so do not use the original source. Instead, you must only use the one I provide via the DLE.

- The application is intended for use by members of the public who are interested in analysing

- Airbnb data as well as Airbnb staff who are responsible for making sure data is accurate. The assignment will focus on data collected for New York City.

- All admin staff at the Airbnb office will use the system in the same way. They will need to be able to do the following tasks:

 ###DISTRICT
 -  SEARCH and DISPLAY district data appropriately
 -  ADD a new district
 -  EDIT a district name
 
 ###NEIGHBOURHOOD
 - SELECT and DISPLAY neighbourhood data appropriately
 - ADD a new neighbourhood
 - EDIT a neighbourhood name
 
 ###PROPERTY
 - SELECT and DISPLAY a property
 - ADD a new property
 - EDIT/DELETE an existing property

A __District__ is defined by the following characteristics or features:
- District name
- The number of neighbourhoods in that district
- All the neighbourhoods within that district (an array of __Neighbourhoods__).

A __Neighbourhood__ object is defined by the following features/characteristics:
- Neighbourhood name
- The number of properties in that Neighbourhood
- All the properties within that neighbourhood (an array of __Properties__)

A __Property__ is defined by the following features/characteristics:
- Property ID
- Property Name
- Host ID
- Host Name
- Number of properties listed for this host
- Latitude
- Longitude
- Room Type
- Price
- Minimum number of nights
- Availability number of days; out of 365 days per year)

## You Must Not Take 'Shortcuts'
- The data must be stored in a single array.
 - You may not use the <LIST> or other collections supported by C# (sorry). 
 - If using arrays, then only the .Length property may be used.
- The application should not make use of C# build in functions where their use is solely to provide you with a shortcut; to enable you to avoid coding the functionality yourself.
 - __get; set;__ shortcuts in class declarations (If you don’t know what this means, you are
already not using this shortcut!)
 - the __.Contains__ in the search functionality to you code
 
- If you are coding the EXTENDED application then do not use any C# chart components for drawing graphs.

- For the bars use the standard graphics methods of DrawRectangle() or FillRectangle().
https://msdn.microsoft.com/enus/library/system.drawing.graphics%28v=vs.110%29.aspx
- Please do not hard code an absolute path to the folder which contains the data files.
  - An example of an absolute path is something like:
“C:\Users\Documents\Assignment\heathrow.txt”.
  - Use a file dialog component instead.



# Assessment Criteria
This assignment will be assessed on the following:
- Program __design__ - program interface is easy to use and provides an appropriate quantity of user feedback!
- Program __execution__ - program executes correctly without errors program runs in a logical manner
- Program __clarity__ - suitable names for variables etc., appropriate use of comments*,  general layout (indenting etc.), suitable names for forms and components
- C# facilities - appropriate use of interface components, appropriate use of language overall
- You __must not change the structure__ of the input data file (of course you can change the data by editing data and adding data as specified in the functionality lists). However, please bring the original data file with your to the viva. I reserve the right to ask you to use a different data file on the day of your viva. You have been warned.
- Additionally, __you must not add__ to the functionality of this brief. However, do please
demonstrate your programming flair by using
  - good __quality__ code - comments, consistent indentation, well named variables 
  - input __validation__
 - __ modularity -__ use shorter methods and pass parameters rather than great big long
event procedures.
 - a __consistent interface__ across the whole product – this product should not look like it
was programmed by four different people
 - gathering/using __user feedback__ to improve your product
