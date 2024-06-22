# Dogs Journey

This application is for Dog lovers. If you want to keep track of the tricks your pup has learnt. You came to the right place.

# Running this project 
- Project > DogTricksApplication Properties > Change target framework to 4.7.1 -> Change back to 4.7.2
- Make sure there is an App_Data folder in the project (Right click solution > View in File Explorer)
- Tools > Nuget Package Manager > Package Manage Console > Update-Database
- Check that the database is created using (View > SQL Server Object Explorer > MSSQLLocalDb > ..)
- Run API commands through CURL to create new dogs

# Functionality
## Dog
- Full Crud operations for dogs
- track your dogs name, breed,birthday, age
- add tricks they learnt at the form on their profile

- ## Trick
- Full Crud operations for tricks
- you can see all the tricks in the tricks catalogue, don't see one you like? You can add one by pasting a youtube link in the Trick/New page.

