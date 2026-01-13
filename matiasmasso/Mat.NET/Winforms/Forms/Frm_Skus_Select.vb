Public Class Frm_Skus_Select
    Private _selectedItem As DTOProductSku

    Public Event onItemSelected(sender As System.Object, e As MatEventArgs)


    Public Sub New(value As List(Of DTOProductSku))
        MyBase.New()
        Me.InitializeComponent()
        Xl_Skus1.Load(value, oSelectionMode:=DTO.Defaults.SelectionModes.Selection)

        Me.Width = Me.Width + Xl_Skus1.WidthAdjustment
        Me.Height = Xl_Skus1.AdjustedHeight
    End Sub

    Public ReadOnly Property SelectedObject As DTOProductSku
        Get
            Dim retval As DTOProductSku = _selectedItem
            Return retval
        End Get
    End Property


    Private Sub Xl_Skus1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Skus1.onItemSelected
        _selectedItem = e.Argument
        RaiseEvent onItemSelected(sender, e)
        Me.Close()
    End Sub
End Class