Public Class Menu_EdiRemadvItem
    Private _EdiRemadvItem As EdiRemadvItem

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEdiRemadvItem As EdiRemadvItem)
        MyBase.New()
        _EdiRemadvItem = oEdiRemadvItem
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Assign()})
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

    Private Function MenuItem_Assign() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Asignar"
        Dim oPnds As Pnds = PndsLoader.FromContact(_EdiRemadvItem.Parent.EmisorPago)
        For Each oPnd As Pnd In oPnds
            If oPnd.Amt.Absolute.Equals(_EdiRemadvItem.Amt.Absolute) Then
                Dim oPnd2s As New Pnds
                oPnd2s.Add(oPnd)
                Dim oMatMenuItem As New MatMenuItem("factura " & oPnd.FraNum & " del " & oPnd.Fch.ToShortDateString & " vto " & oPnd.Fch.ToShortDateString, oPnd)
                AddHandler oMatMenuItem.Click, AddressOf Do_AssignPnd
                oMenuItem.DropDownItems.Add(oMatMenuItem)
            Else
                For Each oPnd2 As Pnd In oPnds
                    If Not oPnd.Guid.Equals(oPnd2.Guid) Then
                        Dim oSum As Amt = oPnd.Amt.Clone
                        oSum.Add(oPnd2.Amt)
                        If oSum.Absolute.Equals(_EdiRemadvItem.Amt.Absolute) Then
                            Dim oPnd2s As New Pnds
                            oPnd2s.Add(oPnd)
                            oPnd2s.Add(oPnd2)
                            Dim oMatMenuItem As New MatMenuItem("fra " & oPnd.FraNum & " del " & oPnd.Fch.ToShortDateString & " vto " & oPnd.Fch.ToShortDateString & " + fra " & oPnd2.FraNum & " del " & oPnd2.Fch.ToShortDateString & " vto " & oPnd2.Fch.ToShortDateString, oPnd2s)
                            AddHandler oMatMenuItem.Click, AddressOf Do_AssignPnd
                            oMenuItem.DropDownItems.Add(oMatMenuItem)
                        End If
                    End If

                Next
            End If

        Next
        Return oMenuItem
    End Function





    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As MatEventArgs)
        'Dim oFrm As New Frm_EdiRemadvItem(_EdiRemadvItem)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Do_AssignPnd(ByVal sender As Object, ByVal e As EventArgs)
        Dim oMatMenuItem As MatMenuItem = sender

        Dim oPnds As Pnds = oMatMenuItem.CustomObject
        _EdiRemadvItem.Pnd = oPnds(0)
        Dim exs as New List(Of exception)
        If EdiRemadvLoader.Update(_EdiRemadvItem.Parent, exs) Then
            RefreshRequest(sender, MatEventArgs.Empty)
        Else
            UIHelper.WarnError( exs, "error al desar la remesa")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
