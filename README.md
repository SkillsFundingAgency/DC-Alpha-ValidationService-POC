# Introduction 
The repo was produced during the alpha phase of the new data collections services by the ESFA.
One of the goals of the project is to address certain aspects of application lifecycle management. This alpha repo looks at what the developer experience 
is when developing validation rules in C#.
It also looks at performance aspects and uses a real worl domain model to represent the data.
It can also connect to SQL PaaS data sources for validation data (learning aims).

Finally, it explores what is required to generate good quality test data in an automated fashion. This data generation can then plug into the
acceptance tests on a build server.

# Getting Started
We deliberately remove the "PrivateSettings.config" and "PrivateConnectionStrings.config" (as they contain
SAS keys to our azure subscription).
Add (in DCT.ValidationService.Web) these settings.


# Build and Test
The DCT.ValidationService.Web needs to be hosted (right click publish) to host your web application.
You will need the two config files referenced above. Blank examples are included.
ULNv2 and LARS are databases (LARS can be obtained from FIS downloads). ULNs needs to be created and populated with a list of test
ULNs. See "ListOfULNs.cs" to see how you can generate test ULN's easily.

# Contribute
This is an alpha repo and no longer maintained. The learning points from this repo are now factored into our
beta code (which is maintained and published on github).
