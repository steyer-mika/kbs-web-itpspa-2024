# FunEvents GmbH - Eventverwaltungssoftware

Die FunEvents GmbH vermittelt Veranstaltungen im Bereich Extremsport und stellt die Verbindung zwischen Kunden und Anbietern dar. Um den gestiegenen Anforderungen gerecht zu werden, wird eine Software entwickelt, um die Verwaltung aller notwendigen Informationen seitens des Kunden und dessen Eventbuchungen zu übernehmen. Das Ziel ist eine Effizienzsteigerung gegenüber der bisherigen Abwicklung.

## Use Cases und Funktionen

1. **Kunde bucht ein Event**
   - *Ziel:* Der Kunde wählt sein gewünschtes Event aus der Eventsliste aus. Die Daten der Buchung werden erfasst.

2. **Buchung wird abgelehnt**
   - *Ziel:* Die Webanwendung überprüft, ob für den gewünschten Event noch Plätze frei sind. Sollten keine Plätze mehr frei sein, so erhält der Kunde eine Ablehnung. Die Webanwendung informiert entsprechend den Kunden.

3. **Reservierung für Event ausführen**
   - *Ziel:* Der Buchungswunsch des Kunden wird in das System übernommen.

4. **Gebuchte Events ansehen**
   - *Ziel:* Die Webanwendung prüft, ob Events gebucht worden sind.

5. **Kunde storniert eine Buchung**
   - *Ziel:* Der Kunde wählt ein gebuchtes Event aus der Eventsliste aus und storniert dieses.

6. **Rechnung über Stornogebühr bekommen**
   - *Ziel:* Bei erfolgreicher Stornierung eines Events durch einen Kunden wird eine Stornogebühr von 10% des Buchungspreises gestellt.

7. **Login**
   - *Ziel:* Die Daten des Kunden werden gesucht und verifiziert. Der Kunde wird erfolgreich eingeloggt und hat Zugriff auf das Dashboard.

8. **Registrieren**
   - *Ziel:* Ein Neukunde wird im System angelegt.

9. **Passwort ändern**
   - *Ziel:* Die Daten eines bestehenden Kunden müssen aufgrund von Änderungen aktualisiert werden.

10. **Kontaktdaten aufrufen**
    - *Ziel:* Der Besucher erhält eine Übersicht zu den Kontaktdaten der FunEvents GmbH.

11. **Informationen zur FunEvents aufrufen**
    - *Ziel:* Kunden über wichtige Details der FunEvents GmbH informieren.

12. **Informationen für Events aufrufen**
    - *Ziel:* Den Kunden über die Events informieren.

## Installation

1. Klonen Sie das Repository: `git clone <repository-url>`
2. Navigieren Sie in das Projektverzeichnis: `cd <project-directory>`
3. Führen Sie `dotnet build` aus, um das Projekt zu kompilieren.
4. Führen Sie `dotnet run` aus, um die Anwendung zu starten.
