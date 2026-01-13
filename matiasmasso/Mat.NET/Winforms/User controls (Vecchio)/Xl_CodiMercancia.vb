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



    Private Sub OnCodiUpdated(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCodi As MaxiSrvr.CodiMercancia = CType(sender, MaxiSrvr.CodiMercancia)
        TextBoxId.Text = oCodi.Id
        TextBoxDsc.Text = oCodi.Dsc
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
