# M09 - T2. Processos: 2a Convocatòria  
## Recull d'activitats

---

### **Exercici 1 – Programació distribuïda (2 punts)**

**Enunciat:**  
Cerca un mínim de 5 aplicacions o tecnologies que fan servir programació distribuïda. Explica què fan i per què necessiten ser distribuïdes.

**Resposta:**

1. **Google Search**  
   Necessita distribuir les cerques entre milers de servidors per poder processar milions de peticions per segon i oferir resultats ràpids i rellevants.

2. **Netflix**  
   Utilitza una arquitectura distribuïda per servir continguts multimèdia a escala global, aprofitant servidors repartits arreu del món per minimitzar la latència.

3. **Amazon Web Services (AWS)**  
   Plataforma de serveis al núvol que ofereix recursos de càlcul, emmagatzematge i bases de dades distribuïdes per escalar segons demanda.

4. **Bitcoin**  
   Xarxa descentralitzada on cada node manté una còpia del llibre de transaccions (blockchain), evitant un punt únic de fallada.

5. **Apache Hadoop**  
   Marc de treball per al processament de grans volums de dades en clústers de servidors, distribuint la càrrega de treball i emmagatzematge.

---

### **Exercici 2 – Concurrència en CPU multicore (2 punts)**

**Enunciat:**  
Explica de quines formes s’aplica la concurrència en les CPU’s de més d’un nucli actualment. Raona quins són els avantatges d’aplicar cada tipus.

**Resposta:**

1. **Multithreading**  
   Permet executar diversos fils dins un mateix procés. Avantatge: millora l’eficiència compartint context de procés.

2. **Multiprocessing**  
   Cada nucli pot executar un procés diferent. Avantatge: millora el rendiment general i pot executar processos aïllats simultàniament.

3. **Hyper-threading (Intel)**  
   Simula dos fils per nucli físic. Avantatge: millora el rendiment en aplicacions optimitzades per multitarea.

4. **Task Scheduling**  
   Els sistemes operatius reparteixen les tasques eficientment entre nuclis. Avantatge: maximitza l’ús dels recursos.

---

### **Exercici 3 – Paral·lelisme vs Asincronia (4 punts)**

**Enunciat:**

a. Enumera i explica les diferències entre elles.  
b. Explica cada pas del cicle de vida d’un mètode asíncron.  
c. De la següent llista d’aplicacions, indica quin tipus de programació faries servir.

#### a. Diferències:

| Característica            | Programació Paral·lela                  | Programació Asíncrona                     |
|--------------------------|-----------------------------------------|-------------------------------------------|
| Objectiu                 | Executar tasques alhora                 | No bloquejar mentre s’espera              |
| Ús típic                 | Càlculs intensius                       | IO: xarxa, fitxers                        |
| Execució                 | En múltiples fils/CPU                   | Pot usar un sol fil                       |
| Exemple                  | Renderitzar imatges                     | Llegir fitxer sense bloqueig              |

#### b. Cicle de vida mètode asíncron:

1. S'invoca el mètode.
2. Comença execució fins a trobar `await`.
3. El fil retorna el control al sistema.
4. Quan la tasca asincrònica acaba, es reprèn l'execució.
5. Finalitza el mètode.

#### c. Classificació d'aplicacions:

- **Processament de lots d’imatges:**  
  ➤ Programació **paral·lela** (diversos fils per imatges).

- **Aplicació d’escriptori amb UI:**  
  ➤ Programació **asíncrona** (evitar bloquejos visuals).

- **Aplicació de missatgeria en temps real:**  
  ➤ Programació **asíncrona** (resposta immediata d’entrada/sortida).

- **Renderització de gràfics en 3D:**  
  ➤ Programació **paral·lela** (divisió de treball de renderització).

---

### **Exercici 4 – Llistat de processos i fils (4 punts)**

**Solució:**  
El codi d’aquest exercici es troba a la solucio del main [`Exercici4/Main.cs`](./M09.T2.Activities.MiquelManzano/Ex4/Program.cs).  
Implementa l’ús de la classe `Process` per llegir els processos i fils, i guarda el resultat en un `.txt`.

---

### **Exercici 5 – Càrrega de dispositius amb bateria (12 punts)**

El codi complet per a aquest exercici es troba a la solucio del main [`Exercici5/Main.cs`](./M09.T2.Activities.MiquelManzano/M09.T2.Activities.MiquelManzano/Program.cs).  
Inclou dues simulacions completes amb execució paral·lela dels dispositius.

---

### **Exercici 6 – Producció d’un pastís: seqüencial vs paral·lel (12 punts)**
**Solució:**  
El codi complet d’aquest exercici es troba a [`Exercici6/Main.cs`](./M09.T2.Activities.MiquelManzano/Ex6/Program.cs).  
Conté les dues simulacions (seqüencial i paral·lela) i mostra el temps total de cada una.

---

### **Exercici 7 – Revisió de codi amb Threads (4 punts)**

**a. Errors detectats:**
- Stopwacth mal escrit.
- sW.StarNew() no existeix.
- Accés a GlobalMax i GlobalMin sense sincronització.
- No s’utilitza Thread.Join() per esperar els fils.
- Random compartit entre fils (no és segur).

**b. Codi corregit:**

```csharp
using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    public static int[] Readings;
    public static int GlobalMax = int.MinValue;
    public static int GlobalMin = int.MaxValue;
    static object locker = new();

    static void Main()
    {
        Console.Write("Introdueix el nombre de sensors: ");
        int sensors = int.Parse(Console.ReadLine());
        Stopwatch sw = Stopwatch.StartNew();

        Readings = new int[sensors];
        Thread[] threads = new Thread[sensors];

        for (int i = 0; i < sensors; i++)
        {
            int id = i;
            threads[i] = new Thread(() =>
            {
                var localRandom = new Random(Guid.NewGuid().GetHashCode());
                for (int j = 0; j < 100000; j++)
                {
                    int value = localRandom.Next(-20, 51);
                    Readings[id] = value;

                    lock (locker)
                    {
                        if (value > GlobalMax) GlobalMax = value;
                        if (value < GlobalMin) GlobalMin = value;
                    }
                }
            });
            threads[i].Start();
        }

        foreach (var t in threads) t.Join();

        sw.Stop();
        Console.WriteLine($"Final – Max: {GlobalMax}, Min: {GlobalMin}");
        Console.WriteLine($"Temps total: {sw.ElapsedMilliseconds} ms");
    }
}

```

---

### Creat per

- **Miquel Manzano** - [@miquel-manzano](https://github.com/miquel-manzano)

```
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡀⠀⠀⠀⠀
⠀⠀⠀⠀⢀⡴⣆⠀⠀⠀⠀⠀⣠⡀⠀⠀⠀⠀⠀⠀⣼⣿⡗⠀⠀⠀⠀
⠀⠀⠀⣠⠟⠀⠘⠷⠶⠶⠶⠾⠉⢳⡄⠀⠀⠀⠀⠀⣧⣿⠀⠀⠀⠀⠀
⠀⠀⣰⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣤⣤⣤⣤⣤⣿⢿⣄⠀⠀⠀⠀
⠀⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣧⠀⠀⠀⠀⠀⠀⠙⣷⡴⠶⣦
⠀⠀⢱⡀⠀⠉⠉⠀⠀⠀⠀⠛⠃⠀⢠⡟⠀⠀⠀⢀⣀⣠⣤⠿⠞⠛⠋
⣠⠾⠋⠙⣶⣤⣤⣤⣤⣤⣀⣠⣤⣾⣿⠴⠶⠚⠋⠉⠁⠀⠀⠀⠀⠀⠀
⠛⠒⠛⠉⠉⠀⠀⠀⣴⠟⢃⡴⠛⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠛⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
```

---