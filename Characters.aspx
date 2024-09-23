<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Characters.aspx.cs" Inherits="API_MARVEL.Characters" Async="true" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Marvel Characters</title>
    <link rel="stylesheet" href="Assets/styles.css">
</head>
<body>
    <div class="container">
        <h1>Marvel Characters</h1>
        
        <!-- Filtro -->
        <form method="get" action="Characters.aspx" class="filter-form">
            <input type="text" name="filter" placeholder="Search by name" value="<%= Request.QueryString["filter"] %>" />
            <input type="submit" value="Search" />
        </form>

        <!-- Grid de personajes -->
        <div id="charactersGrid" class="character-grid" runat="server"></div>

        <!-- Paginación -->
        <div id="paginationContainer" class="pagination-container" runat="server"></div>
    </div>
</body>
</html>
