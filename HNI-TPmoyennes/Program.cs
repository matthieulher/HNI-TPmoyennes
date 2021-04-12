using System;
using System.Collections.Generic;
using System.Linq;

namespace TPMoyennes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");
            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");
            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");
            Random random = new Random();
            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count; ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count; matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, (float)((6.5 +
                       random.NextDouble() * 34)) / 2.0f));
                        // Note minimale = 3
                    }
                }
            }

            Eleve eleve = sixiemeA.eleves[6];
            // Afficher la moyenne d'un élève dans une matière
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            eleve.Moyenne(1) + "\n");
            // Afficher la moyenne générale du même élève
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.Moyenne() + "\n");
            // Afficher la moyenne de la classe dans une matière
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            sixiemeA.Moyenne(1) + "\n");
            // Afficher la moyenne générale de la classe
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne Generale : " + sixiemeA.Moyenne() + "\n");
            Console.Read();
        }
    }
}
// Classes fournies par HNI Institut
class Note
{
    public int matiere { get; private set; }
    public float note { get; private set; }
    public Note(int m, float n)
    {
        matiere = m;
        note = n;
    }
}

class Classe
{
    //Attributs
    public string nomClasse { get; private set; }
    public List<Eleve> eleves { get; private set; }
    public List<Matiere> matieres { get; private set; }

    //Méthodes
    public void ajouterEleve(string n, string p) 
    {
        if (eleves.Count == 30)
        {
            throw new Exception();
        }

        eleves.Add(new Eleve(n, p));
    }
    public void ajouterMatiere(string m)
    {
        if (matieres.Count == 10)
        {
            throw new Exception();
        }

        matieres.Add(new Matiere(m));
    }

    //Moyenne Générale Classe
    public Moyenne Moyenne()
    {
        if (eleves.Count == 0)
        {
            throw new Exception();
        }

        float Cmoy = 0.0f;
        for(int i=0; i !=eleves.Count; ++i)
        {
            Cmoy += eleves[i].Moyenne().moyenne;
        }
        return new Moyenne(Cmoy/eleves.Count);
    }

    //Moyenne Classe par Matière
    public Moyenne Moyenne(int m)
    {
        if (eleves.Count == 0)
        {
            throw new Exception();
        }

        float Cmoy=0.0f;
        for (int i=0; i !=eleves.Count; ++i)
        {
            Cmoy += eleves[i].Moyenne(m).moyenne;
        }
        return new Moyenne(Cmoy/eleves.Count);
    }

    //Constructeur
    public Classe(string CName)
    {
        nomClasse = CName;
        eleves = new List<Eleve>();
        matieres = new List<Matiere>();
    }
    
}
class Eleve
{
    //Attributs
    public string nom { get; private set; }

    public string prenom { get; private set; }

    public List<Note> notes { get; private set; }

    private List<Tuple<int, int>> matieres { get; set; }

    //Méthodes
    public void ajouterNote(Note n)
    {
        if (notes.Count == 200)
        {
            throw new Exception();
        }

        notes.Add(n);
        for (int i=0; i !=matieres.Count; ++i)
        {
            if (matieres[i].Item1==n.matiere)
            {
                matieres[i] = new Tuple<int, int>(n.matiere, matieres[i].Item2+1);
                return;
            }
        }
        matieres.Add(new Tuple<int, int>(n.matiere, 1));
    }

    //Moyenne Générale Eleve
    public Moyenne Moyenne()
    {
        if(matieres.Count==0)
        {
            throw new Exception();
        } 

        float[] rval= new float[matieres.Count];
        for (int i=0; i != matieres.Count; ++i)
        {
            rval[i] = Moyenne(i).moyenne;
        }

        float r = 0.0f;
        for (int i=0; i !=matieres.Count; ++i)
        {
            r += rval[i];
        }

        return new Moyenne(r/matieres.Count);
    }

    //Moyenne Eleve de chaque matières
    public Moyenne Moyenne(int m)
    {
        int den = 0;
        for (int i = 0; i != matieres.Count; ++i)
        {
            if (m == matieres[i].Item1)
            {
                den = matieres[i].Item2;
            }
        }
        if (den == 0 )
        {
            throw new Exception();
        }

        float rval = 0.0f;
        for (int i = 0; i != notes.Count; ++i)
        {
            if (notes[i].matiere==m)
            {
                rval += notes[i].note;
            }           
        }       
        return new Moyenne(rval/den);
    }

    //Constructeur
    public Eleve(string n, string p)
    {
        nom = n;
        prenom = p;
        notes = new List<Note>();
        matieres = new List<Tuple<int, int>>();
    }
}
class Matiere
{
    public string matiere { get; private set; }

    //Methode
    public override string ToString()
    {
        return matiere;
    }
    public Matiere(string m)
    {
        matiere = m;
    }
}
class Moyenne
{
    public float moyenne { get; private set; }

    public override string ToString()
    {
        return moyenne.ToString("F2");
    }

    //Constructeur
    public Moyenne(float m)
    {
        moyenne = m;
    }
}