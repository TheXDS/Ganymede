# Ganymede's history

Around 2017, I was working on creating a very dynamic application for the company I worked for at the time; that was to have the ability to rapidly evolve and change, with the restrictions of being lean in nature, using WPF and MVVM.

There were MVVM frameworks for WPF at the time already (Prism comes to mind immediately), but the team did not have the time nor the will to start learning it, and licensing professional software toolkits was outside of our reach due to budget constraints.

I got tasked with the creation of some kind of "Shell" where devs could rapidly define a ViewModel, and then have its view resolved and presented. At the end, the project was not particularly successful, nor fast, nor clean, nor stable, nor maintainable... but it did the job well enough for us at the time.

The concept of an open-sourced MVVM framework I owned did not fade away from my mind. I enjoyed the challenge of comming up with elegant solutions to the problems we faced when creating that tool in 2017. At some point in 2020, I decided to re-imagine and re-implement several of the things we made (not just Ganymede).

Ganymede was one of such ideas - the MVVM framework that was in charge of view navigation and resolution (which, by the way, did not really had a name; we just referred to it as the MVVM framework). It was still as dirty, and as unmaintainable as the old project I worked on back in 2017, but it did improve on some fields.

This experimental version added some form of basic UI services (dialogs and a method that disabled the UI while running tasks, as well as some form of primitive navigation) and had a tabbed layout. Again, this mirrored the MVVM framework I wrote previously.

Of course. At some point I grew increasingly dissatisfied with the quality of the code. So I made a first rewrite. It retained the same UI philosophy but with considerably better (albeit, still clunky) code. I wouldn't call this version great by any means, but it was definitely better. This was the 0.2.x series.

The 0.2.x series foolishly included another part of that old project - [Proteus](https://github.com/TheXDS/Proteus). This one *did* have a name, because it was actually a large chunk of the whole app. It was intended to generate Views and ViewModels based on entity definitions, and was intended to rapidly create applications connected to data by just defining the model and configuring the presentation of the properties of each entity.

That was never going to be a viable project as it was. Having too much things on Ganymede was way too much to handle. Eventually, I decided that it needed to be rewritten again. Took a while to happen, but I sat down, torn everything apart and started over one more time. On this occasion, I took more modern UI frameworks as a model, instead of the old and terrible ideas I brought with me from the 2017's project.

And this is how we arrived at the 0.3.x series for Ganymede. In its current iteration, it supports navigation by stack (and other philosophies can be implemented easily), abstraction of many dialog types and several View Resolution types, as well as some useful extras (numeric up-down controls and decorated TextBoxes with watermark support to name a few)