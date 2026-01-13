

Public Class Xl_Adr3
    Private mAdr As Adr

    Public Event AfterUpdateAdr(sender As Object, e As System.EventArgs)
    Public Event AfterUpdateCit(sender As Object, e As System.EventArgs)

    Public Property Adr As Adr
        Get
            Return mAdr
        End Get
        Set(value As Adr)
            If value IsNot Nothing Then
                mAdr = value
                refresca()
            End If
        End Set
    End Property

    Private Sub refresca()
        TextBoxAdr.Text = mAdr.ToString
        If mAdr.Zip IsNot Nothing Then
            Dim oCountry As Country = mAdr.Zip.Location.Zona.Country
            If oCountry.Equals(MaxiSrvr.Country.Default) Then
                PictureBoxFlag.Visible = False
            Else
                PictureBoxFlag.Visible = True
                PictureBoxFlag.Image = oCountry.Flag
            End If
        End If
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu_Adr As New Menu_Adr(mAdr)
        AddHandler oMenu_Adr.AfterUpdateAdr, AddressOf RefreshRequestAdr
        AddHandler oMenu_Adr.AfterUpdateCit, AddressOf RefreshRequestCit
        oContextMenu.Items.AddRange(oMenu_Adr.Range)
        TextBoxAdr.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub TextBoxAdr_DoubleClick(sender As Object, e As System.EventArgs) Handles TextBoxAdr.DoubleClick
        Dim oFrm As New Frm_Adr(mAdr)
        AddHandler oFrm.AfterUpdateAdr, AddressOf RefreshRequestAdr
        AddHandler oFrm.AfterUpdateCit, AddressOf RefreshRequestCit
        oFrm.Show()
    End Sub

    Private Sub RefreshRequestAdr(sender As Object, e As System.EventArgs)
        mAdr = CType(sender, Adr)
        RaiseEvent AfterUpdateAdr(mAdr, EventArgs.Empty)
        refresca()
    End Sub
    Private Sub RefreshRequestCit(sender As Object, e As System.EventArgs)
        mAdr = CType(sender, Adr)
        RaiseEvent AfterUpdateCit(mAdr, EventArgs.Empty)
        refresca()
    End Sub
End Class
