Projet réalisé par : Mohamed Elyakoubi Elidrissi & Youssef Elyakoubi Elidrissi

Dans ce projet nous avons essayé de mettre en point les parties suivantes:
- récupérations des villes à partir de SQLite (j'ai inséré quelques unes)
- intégration des algorithmes ( Crossover, elite, mutation)
- front-end : selection des villes insérés dans la base de données depuis la carte
- malheureusement on a pas pu lier le back et le front pour calculer les chemins les plus courts depuis les éléments sélectionné
dans la carte, en revanche dans l'onglet parametrage on définit le nombre de chemins de chaque algorithmes et on peut générer une
listes de chemins optimaux à travers un jeu de données effectué en back-end. ça fonctionne. 
- un petit problème peut surgir : vu que je n'ai pas utiliser les (threads) parfois le programme se bloque et ne charge pas toutes
les données ( après avoir effectué plusieurs test, il se bloque dans la partie de crossover) mais il suffit de relancer le programme
pour que ça marche.
