Public Class Menu_PrInsercio
    Private _PrInsercions As PrInsercions

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oInsert As PrInsercio)
        MyBase.New()
        _PrInsercions = New PrInsercions
        _PrInsercions.Add(oInsert)
    End Sub

    Public Sub New(ByVal oInserts As PrInsercions)
        MyBase.New()
        _PrInsercions = oInserts
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), MenuItem_factura()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_factura() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "factura"
        AddHandler oMenuItem.Click, AddressOf Do_Factura
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPrInsercio As PrInsercio = _PrInsercions.First
        PrInsercioLoader.Load(oPrInsercio)
        Dim oFrm As New Frm_PrInsercio(oPrInsercio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Sub Do_Factura(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Wz_Proveidor_NewFra(_PrInsercions)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
