Public Class Menu_Staff
    Inherits Menu_Base

    Private _Staff As DTOStaff


    Public Sub New(ByVal oStaff As DTOStaff)
        MyBase.New()
        _Staff = oStaff
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_ImportIrpfCert())
    End Sub


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



    Private Function MenuItem_ImportIrpfCert() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar Certificat Irpf"
        Select Case Current.Session.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts
            Case Else
                oMenuItem.Visible = False
        End Select
        AddHandler oMenuItem.Click, AddressOf Do_ImportIrpfCert
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact(_Staff)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_StaffScheds()
        Dim oFrm As New Frm_StaffScheds(_Staff)
        oFrm.Show()
    End Sub


    Private Sub Do_RegistroJornadaLaboral_CopyLinkEntrada()
        Dim url = MmoUrl.ApiUrl("JornadaLaboral/log", CInt(DTOJornadaLaboral.Modes.entrada), _Staff.Guid.ToString())
        UIHelper.CopyLink(url)
    End Sub

    Private Sub Do_RegistroJornadaLaboral_CopyLinkSortida()
        Dim url = MmoUrl.ApiUrl("JornadaLaboral/log", CInt(DTOJornadaLaboral.Modes.sortida), _Staff.Guid.ToString())
        UIHelper.CopyLink(url)
    End Sub

    Private Async Sub Do_ImportIrpfCert(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oExercici As DTOExercici = DTOExercici.Past(Current.Session.Emp)

        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar Certificat Irpf " & oExercici.Year & " per " & _Staff.Abr
            If .ShowDialog = DialogResult.OK Then
                Dim exs As New List(Of Exception)


                Dim oDocfile = LegacyHelper.DocfileHelper.Factory(.FileName, exs)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oContactDoc As New DTOContactDoc()
                    With oContactDoc
                        .Contact = _Staff
                        .Fch = oExercici.LastFch
                        .Type = DTOContactDoc.Types.Retencions
                        .Ref = "IRPF " & oExercici.Year
                        .DocFile = oDocfile
                    End With

                    If Not Await FEB.ContactDoc.Update(oContactDoc, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End With
    End Sub


End Class


