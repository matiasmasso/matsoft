Public Class Xl_CodiMercancia

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property CodiMercancia() As maxisrvr.CodiMercancia
        Get
            Dim oCodiMercancia As New maxisrvr.CodiMercancia(TextBoxId.Text)
            Return oCodiMercancia
        End Get
        Set(ByVal value As maxisrvr.CodiMercancia)
            If value Is Nothing Then
                TextBoxId.Clear()
                TextBoxDsc.Clear()
            Else
                TextBoxId.Text = value.Id
                TextBoxDsc.Text = value.Dsc
            End If
        End Set
    End Property

    Public Overloads Property enabled() As Boolean
        Get
            Return ButtonSearch.Enabled
        End Get
        Set(ByVal value As Boolean)
            ButtonSearch.Enabled = value
        End Set
    End Property


    Private Sub ButtonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSearch.Click
        Dim oFrm As New Frm_CodisMercancia(Frm_CodisMercancia.Modes.ForSelection)
        AddHandler oFrm.AfterSelect, AddressOf OnCodiUpdated
        oFrm.Show()
    End Sub

    Private Sub TextBoxId_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxId.DoubleClick, TextBoxDsc.DoubleClick
        Dim oFrm As New Frm_CodiMercancia(Me.CodiMercancia)
        AddHandler oFrm.AfterUpdate, AddressOf OnCodiUpdated
        oFrm.Show()
    End Sub

    Private Sub OnCodiUpdated(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCodi As maxisrvr.CodiMercancia = CType(sender, maxisrvr.CodiMercancia)
        TextBoxId.Text = oCodi.Id
        TextBoxDsc.Text = oCodi.Dsc
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
