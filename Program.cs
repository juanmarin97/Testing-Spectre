using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;

namespace TableTest {
    partial class Program {
        static async Task Main (string[] args) {

            string name, surname;
            int birthday;

            List<Person> personsList = new List<Person> ();

            do {
                Console.WriteLine ("1- Para agregar persona\n2- Para mostrar información");
                var option = int.Parse (Console.ReadLine ());
                switch (option) {
                    case 1:

                        name = AnsiConsole.Ask<string> ("Cual es tu [blue]nombre[/]?");
                        surname = AnsiConsole.Ask<string> ("Cual es tu  [blue]apellido[/]?");
                        birthday = AnsiConsole.Ask<int> ("Cual es tu [green]año de nacimiento[/]?");
                        Person persona = new Person (name, surname, birthday);
                        personsList.Add (persona);
                        AnsiConsole.Status ()
                            .Start ("Guardando...", ctx => {
                                Thread.Sleep (1000);
                                // Update the status and spinner
                                ctx.Status ("Finalizado");
                                ctx.Spinner (Spinner.Known.Star);
                                ctx.SpinnerStyle (Style.Parse ("green"));
                                Thread.Sleep (1000);
                            });
                        Console.Clear ();
                        break;
                    case 2:

                        var saveTable = AnsiConsole.Confirm ("Guardar tabla?");
                        AnsiConsole.Record ();
                        Table table = new Table ();

                        table.AddColumn ("Nombre");
                        table.AddColumn ("Apellido");
                        table.AddColumn ("Edad");
                        table.AddColumn ("Pais");
                        table.AddColumn ("Ciudad");
                        table.AddColumn ("Idioma");
                        table.AddColumn ("Cordenadas");

                        foreach (var item in personsList) {
                            table.AddRow (item.Name, item.Surname, item.Birthday.ToString (), item.Country, item.City, item.Language, item.Cordinates);
                        }

                        var title = new TableTitle ("Empleado", Style.WithDecoration (Decoration.Bold));
                        table.Title = title;

                        table.Border (TableBorder.Rounded);
                        table.BorderColor<Table> (Color.Blue);
                        AnsiConsole.Render (table);

                        if (saveTable) {
                            string html = AnsiConsole.ExportHtml ();
                            Directory.CreateDirectory ("../Employes");
                            File.WriteAllText ("../Employes/index.html", html);

                            Console.ForegroundColor = Color.Green;
                            Console.WriteLine ("Guardado Exitosamente");
                            Console.ForegroundColor = Color.White;
                        }

                        break;

                    default:
                        break;
                }

            } while (true);
        }
    }
}