# WeddingPlanner
<p>
      Wedding planner is created with Entity Framework, .NET 6,and MVC. It is a project I am usign to learn CRUD operations. CRUD stands for create, read, update and delete.
   Thes operations are used all the time with databases.
</p>

## Google Maps API
<p>
      Additionally, with this project I learned how to use the Google Maps API and got it working with the addresss that is provided to the Add Wedding section.
  Furthermore, I was able to make a directions sections.
</p>

## Future Google Map API sections
<p>
      I will eventuallly add a print me on the directions because not everyone likes using their GPS on their phone.
</p>

## Currently
<p>
      Currently I am working on updating a section of the CRUD procedures. I have it working but I am trying to figure out how to have my custom validation show in the front end validation like the other validations. When you put the validations on the client side your pages run smoother and quicker. However, you also have them in the back end just in case. 

</p>

## Future Goals
<p>
      I am planning to add a delete method that will delete all weddings they are in the pass. Example, you added a wedding yesterday that happened at 6pm today, I want the method to recognize the dates and see which one already happed, then delete the weddings that already happened in the past. 
</p>

```cs
// this goes at the bottom of the views/cshtml files so that they can use your server side validations in the client side. 
// Why have them all go to the rear.
@section Scripts
{
    @{
    <partial name="_ValidationScriptsPartial" />
    }
}
```
