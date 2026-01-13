Public Class Xl_Comarcas
    Inherits ComboBox

    Private _values As List(Of DTOComarca)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oComarcas As List(Of DTOComarca), Optional InsertNoValue As Boolean = False)
        _values = oComarcas
        If InsertNoValue Then
            Dim oNoComarca As New DTOComarca(Guid.Empty)
            oNoComarca.Nom = Current.Session.Tradueix("(seleccionar comarca)", "(sel·leccionar comarca)", "(select area)", "(seleccionar comarca)")
            _values.Insert(0, oNoComarca)
        End If
        MyBase.DataSource = _values
        MyBase.DisplayMember = "Nom"
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Comarca As DTOComarca
        Get
            Return CurrentComarca()
        End Get
    End Property

    Private Function CurrentComarca() As DTOComarca
        Dim retval As DTOComarca = MyBase.SelectedItem
        If retval IsNot Nothing Then
            If retval.Guid.Equals(Guid.Empty) Then retval = Nothing
        End If
        Return retval
    End Function

    Private Sub Xl_Comarcas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(CurrentComarca))
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oComarca As DTOComarca = CurrentComarca()

        If oComarca IsNot Nothing Then
            Dim oMenu_Comarca As New Menu_Comarca(oComarca)
            oContextMenu.Items.AddRange(oMenu_Comarca.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Xl_Comarcas_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oFrm As New Frm_Comarca(CurrentComarca)
        AddHandler oFrm.AfterUpdate, AddressOf onComarcaUpdate
        oFrm.Show()
    End Sub

    Private Sub onComarcaUpdate(sender As Object, e As MatEventArgs)
        Dim oUpdatedValue As DTOComarca = e.Argument
        Dim oComarca As DTOComarca = _values.FirstOrDefault(Function(x) x.Equals(oUpdatedValue))
        If oComarca IsNot Nothing Then
            oComarca.Nom = oUpdatedValue.Nom
        End If
    End Sub
End Class
