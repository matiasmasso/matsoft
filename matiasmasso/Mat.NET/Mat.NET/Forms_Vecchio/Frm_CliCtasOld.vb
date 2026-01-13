Public Class Frm_CliCtasOld
    Private mContact As Contact = Nothing
    Private mSelectionMode As Modes = Modes.NotSet
    Private Const CTAYEARSEPARATORCHAR As String = "/"

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterSelect(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Enum Modes
        NotSet
        ForSelection
        ForBrowsing
    End Enum

    Public Sub New(ByVal oContact As Contact, Optional ByVal SelectTabPnd As Boolean = False)
        MyBase.New()
        Me.InitializeComponent()
        Cursor = Cursors.WaitCursor
        mContact = oContact
        Me.Text = "COMPTES DE " & mContact.Clx
        Xl_Contact_Logo1.Contact = mContact
        Xl_ContactCtas1.Contact = mContact

        Me.Show()
        Application.DoEvents()

        If SelectTabPnd Or mContact.Client.Exists Or mContact.Proveidor.Exists Then
            Dim oPagePnd As New TabPage("PENDENT")
            Dim oPnds As New Xl_Contact_Pnds_Old
            AddHandler oPnds.AfterUpdate, AddressOf RefreshRequest
            oPnds.Contact = mContact
            oPnds.Dock = DockStyle.Fill
            oPagePnd.Controls.Add(oPnds)
            TabControl1.TabPages.Add(oPagePnd)
            If SelectTabPnd Then TabControl1.SelectedTab = oPagePnd
        End If
        Cursor = Cursors.Default
    End Sub

    Public Sub New(ByVal oCcd As Ccd, Optional ByVal oSelectionMode As Modes = Modes.ForBrowsing)
        MyBase.New()
        Me.InitializeComponent()
        Cursor = Cursors.WaitCursor
        Me.Show()
        mContact = oCcd.Contact
        mSelectionMode = oSelectionMode
        If mContact Is Nothing Then
            Me.Text = "COMPTE " & oCcd.Cta.FullNom
        Else
            Me.Text = "COMPTES DE " & mContact.Clx
            Xl_Contact_Logo1.Contact = mContact
            Xl_ContactCtas1.Contact = mContact

            If New Client(mContact.Guid).Exists Then
                Dim oPagePnd As New TabPage("PENDENT")
                Dim oPnds As New Xl_Contact_Pnds_Old
                AddHandler oPnds.AfterUpdate, AddressOf RefreshRequest
                oPnds.Contact = mContact
                oPnds.Dock = DockStyle.Fill
                oPagePnd.Controls.Add(oPnds)
                TabControl1.TabPages.Add(oPagePnd)
            End If
        End If

        NavigateCta(oCcd, EventArgs.Empty)
        Cursor = Cursors.Default
    End Sub

    Private Function GetContactMenu() As ContextMenuStrip
        Dim oMenu As New ContextMenuStrip
        Dim oMenuContact As New Menu_Contact(mContact)
        oMenu.Items.AddRange(oMenuContact.Range)
        Return oMenu
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub SelectRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterSelect(Me, e)
    End Sub

    Private Sub Xl_ContactCtas1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactCtas1.DoubleClick
        NavigateCta(sender, e)
    End Sub

    Private Sub NavigateCta(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCcd As Ccd = DirectCast(sender, Ccd)

        Dim sTit As String = TabCaption(oCcd)
        Dim iTab As Integer = 0
        For iTab = 0 To TabControl1.TabCount - 1
            If TabControl1.TabPages(iTab).Text.Contains(CTAYEARSEPARATORCHAR) Then
                Select Case TabControl1.TabPages(iTab).Text
                    Case Is < sTit
                    Case Is = sTit
                        TabControl1.SelectedIndex = iTab
                        Exit Sub
                    Case Is > sTit
                        Exit For
                End Select
            End If
        Next

        Dim oControl As New Xl_Ccd_Extracte()
        oControl.SelectionMode = mSelectionMode
        oControl.Ccd = oCcd
        oControl.Dock = DockStyle.Fill
        AddHandler oControl.AfterUpdate, AddressOf RefreshRequest
        AddHandler oControl.NavigateYear, AddressOf NavigateCta
        AddHandler oControl.AfterSelect, AddressOf SelectRequest

        Dim oTabPage As New TabPage(sTit)
        oTabPage.Controls.Add(oControl)

        If iTab < TabControl1.TabCount Then
            TabControl1.TabPages.Insert(iTab, oTabPage)
        Else
            TabControl1.TabPages.Add(oTabPage)
        End If

        TabControl1.SelectedTab = oTabPage
    End Sub

    Private Function TabCaption(ByVal oCcd As Ccd) As String
        Dim s As String = oCcd.Cta.Id.ToString & CTAYEARSEPARATORCHAR & oCcd.Yea.ToString
        Return s
    End Function
End Class