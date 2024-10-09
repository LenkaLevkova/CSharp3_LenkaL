Interakce s REST API
Web API endpoint:
- url, ktory reprezentuje konkretny zdroj (data nebo sluzbu)
- kazdy zdroj ma unikatry endpoint
- napr. u eshopu - product, users, cart, payment....
- my budeme mat iba jeden endpoint ToDoItems - budeme tam robit vsetko - vytvarat. mazat, updatovat...
- a ukoly budu mat ID: /ToDoItems/1 - pri get, put, delete
- my si zavolame GET a nam sa vratia vsetky ukoly vo forme json (response body)
- DTO = Data Transfer Object = to, co sa posiela hore-dole :-)
- 
