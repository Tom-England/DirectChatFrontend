# Direct Chat Frontend

This is an example front end client for the Direct Chat framework using Xamarin.Forms.

This app was created for my B.Sc Computer Science dissertation at Newcastle University. 

### Building the Project

To build this project, certain files must first be added.
Place all .cs files from https://github.com/Tom-England/DirectChatBackend into DirectChat/DirectChat (Should be in the same folder as App.xaml.cs or in a subfolder deeper than this point.)

You will also need to add Curve25519.cs from https://github.com/hanswolff/curve25519 here too as the project will not build using the NuGet version unfortunately.

## Usage
To use this once built, first set up a middleman from the backend and set up the appropriate port forwarding. From here, clients may connect with eachother using their Guid. 


Tom England, 2022