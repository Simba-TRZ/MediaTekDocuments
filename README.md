# MediaTekDocuments — Atelier 2

> Le dépôt d'origine se trouve ici : https://github.com/CNED-SLAM/MediaTekDocuments  
> Il contient dans son readme la présentation complète de l'application d'origine.

## Présentation

Ce dépôt contient l'application **MediaTekDocuments** enrichie dans le cadre de l'Atelier 2 du BTS SIO SLAM (CNED 2026). L'application permet la gestion d'une médiathèque : livres, DVD et revues.

## Fonctionnalités ajoutées

### Gestion des commandes
- Commandes de livres et DVD avec suivi en 4 étapes : En cours → Relancée → Livrée → Réglée
- Création automatique des exemplaires lors du passage à l'état "Livrée" (trigger MySQL)
- Suppression en cascade des exemplaires si la commande est supprimée

### Gestion des abonnements revues
- Ajout, consultation et suppression d'abonnements
- Vérification qu'une parution ne tombe pas dans la période d'un abonnement avant suppression
- Alerte automatique au démarrage si un abonnement expire dans moins de 30 jours

### Suivi des exemplaires
- Affichage de la liste des exemplaires par document (numéro, date achat, état)
- Modification de l'état d'un exemplaire
- Suppression d'un exemplaire

### Authentification et droits
- Fenêtre de connexion par login/mot de passe
- Mots de passe hachés avec BCrypt
- Droits d'accès selon le service :
  - **Commandes** : accès complet
  - **Prêt** : boutons Commandes et Abonnements masqués
  - **Culture** : accès refusé

### Sécurité et qualité
- Credentials déplacés dans `App.config`
- Corrections SonarLint : catches vides, méthodes static, constantes
- Logs avec NLog

### Tests
- 8 tests unitaires MSTest (Livre, Dvd, Revue, ParutionDansAbonnement)
- Collection Postman avec 9 tests (tous PASSED)

---

<img width="1440" height="900" alt="Screenshot 2026-03-30 at 02 45 27" src="https://github.com/user-attachments/assets/feea364d-8488-4b79-9637-cf7df478592c" />


<img width="1440" height="900" alt="Screenshot 2026-03-26 at 01 52 39" src="https://github.com/user-attachments/assets/d009ac2d-06f5-4745-ac77-474f643b99d8" />


<img width="1440" height="900" alt="Screenshot 2026-03-23 at 11 53 46" src="https://github.com/user-attachments/assets/093ff73d-083e-485f-978b-d37ba15b8622" />


---

## Mode opératoire — Installation en local

### Prérequis
- Windows 10 ou supérieur
- .NET Framework 4.7.2
- MAMP (ou équivalent) avec MySQL
- L'API rest_mediatekdocuments déployée en local ou en ligne

### Installation via l'installeur
1. Télécharger `Setup_MediaTekDocuments.exe` depuis ce dépôt (dossier `MediaTekDocuments/`)
2. Double-cliquer sur l'installeur et suivre les étapes
3. L'application s'installe dans `C:\Program Files (x86)\MediaTekDocuments\`

### Configuration
1. Ouvrir `MediaTekDocuments.exe.config` dans le dossier d'installation
2. Modifier l'URL de l'API :
```xml
<add key="uriApi" value="http://VOTRE_IP:8888/rest_mediatekdocuments/" />
```
3. Modifier les credentials si nécessaire :
```xml
<add key="authentification" value="admin:adminpwd" />
```

### Base de données
Le script SQL se trouve dans ce dépôt : `MediaTekDocuments/mediatek86.sql`<br>
Importer ce script dans phpMyAdmin avant de lancer l'application.

### Utilisation
1. Lancer l'application
2. Se connecter avec un compte utilisateur (ex : login `admin`, mot de passe `adminpwd`)
3. Naviguer entre les onglets Livres, DVD, Revues, Parutions des revues

## API en ligne
L'API est déployée à l'adresse : **http://azizmediatek.atwebpages.com/**<br>
Le mode opératoire pour utiliser l'API est disponible dans le dépôt `rest_mediatekdocuments`.

## Documentation technique
La documentation XML générée se trouve dans `MediaTekDocuments/bin/Debug/MediaTekDocuments.xml`

## Dépôt de l'API PHP
https://github.com/Simba-TRZ/rest_mediatekdocuments
