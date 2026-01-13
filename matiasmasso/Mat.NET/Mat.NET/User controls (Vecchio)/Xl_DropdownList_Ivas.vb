Public Class Xl_DropdownList_Ivas
    Private mValuesLoaded As Boolean = False
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property IvaCod() As DTO.DTOTax.Codis
        Get
            If Not mValuesLoaded Then LoadValues()
            Return CurrentCodi()
        End Get
        Set(ByVal value As DTO.DTOTax.Codis)
            If Not mValuesLoaded Then LoadValues()
            For i As Integer = 0 To ComboBox1.Items.Count - 1
                Dim oitem As maxisrvr.MatListItem = ComboBox1.Items(i)
                If oitem.Value = CInt(value) Then
                    ComboBox1.SelectedItem = oitem
                    mAllowEvents = True
                    Exit For
                End If
            Next
        End Set
    End Property


    Private Sub LoadValues()
        Dim oItem As maxisrvr.MatListItem

        oItem = New maxisrvr.MatListItem(DTO.DTOTax.Codis.NotSet, "(seleccionar IVA)")
        ComboBox1.Items.Add(oItem)

        oItem = New maxisrvr.MatListItem(DTO.DTOTax.Codis.Iva_Standard, "estandar")
        ComboBox1.Items.Add(oItem)

        oItem = New maxisrvr.MatListItem(DTO.DTOTax.Codis.Iva_Reduit, "reduit")
        ComboBox1.Items.Add(oItem)

        oItem = New maxisrvr.MatListItem(DTO.DTOTax.Codis.Iva_SuperReduit, "super reduit")
        ComboBox1.Items.Add(oItem)

        mValuesLoaded = True
    End Sub

    Private Function CurrentCodi() As DTO.DTOTax.Codis
        Dim oCodi As DTO.DTOTax.Codis = DTO.DTOTax.Codis.NotSet
        If ComboBox1.SelectedIndex >= 0 Then
            oCodi = ComboBox1.SelectedItem.value
        End If
        Return oCodi
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If mAllowEvents Then
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
        End If
    End Sub
End Class
