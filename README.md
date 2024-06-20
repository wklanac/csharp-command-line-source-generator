# Command Line Source Generator

Under construction.

The goal of this project is to use the Roslyn source generator APIs to automate the creation and maintenance of Command line Interface (CLI) boiletplate code, including the following:
- Command, option, and argument definition
- Connection of handling methods for each command
- Pattern, arity and other basic constraints for arguments provided for commands and options

Target is to support the beta release of the System.CommandLine .NET APIs. Ultimately I'd like this to be in a state where you can just create a partial entry point that the source generator can augment, and implement handlers with the same names as the commands you are using, and everything up until that point is handled for you.
