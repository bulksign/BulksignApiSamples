# Bulksign Api samples
Bulksign API sample code (in C#).

This repository contains sample C# code for interacting with Bulksign API. To make matters very simple is using the [Bulksign dotNet SDK](https://www.nuget.org/packages/BulksignSdk)

### Samples included

SingleDocumentSingleSigner.cs : shows how to send a document for signing with Bulksign for a single recipient. 



### Running the code

- create a [Bulksign](http://bulksign.com) account
- login, go to Settings\Api Token.
- copy the value of the "Default" token
- edit ApiKeys.cs and replace the TOKEN and EMAIL constants with the token value and your email address.
- build and run the project 

