Public Class DTODocRpt
    Property Estilo As Estilos
    Property Papel As FuentePapel
    Property Docs As List(Of DTODoc)

    Public Enum Estilos
        Comanda
        Albara
        Factura
        Proforma
    End Enum

    Public Enum FuentePapel
        Copia
        Original
    End Enum

    Public Enum Ensobrados
        No
        Sencillo
    End Enum

    Public Sub New(ByVal oEstilo As Estilos, Optional ByVal oPapel As FuentePapel = FuentePapel.Copia)
        MyBase.New()
        _Estilo = oEstilo
        _Papel = oPapel
        _Docs = New List(Of DTODoc)
    End Sub

End Class
