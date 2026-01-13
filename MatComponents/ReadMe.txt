per incorporar aquests components en un projecte Blazor,
cal seguir els següents passos:

1) referenciar-lo per project->Add-> Project reference
2) afegir el css al head de Index.Html:
	<link href="_content/MatComponents/matcomponents.css" rel="stylesheet" />
3) afegir el js al final del body de Index.Html:
	<script src="_content/MatComponents/matcomponents.js"></script>
4) afegir al _Imports.razor:
	@using MatComponents
