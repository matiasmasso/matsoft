

Public Class Frm_Telefons
    Private mObjectArrayList As ArrayList
    Private mAllowEvents As Boolean
    Private mSelMode As SelModes

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum SelModes
        JustBrowse
        Missatges
        Timbres
    End Enum

    Private Enum Actions
        GrupsDeResposta
        Linies
        Usuaris
        Festius
        Missatges
        Timbres
    End Enum

    Private Enum LiniaIcons
        Centraleta
        Externa
        Privada
        Obsoleta
    End Enum

    Public Sub New(Optional ByVal oSelMode As SelModes = SelModes.JustBrowse)
        MyBase.New()
        Me.InitializeComponent()
        mSelMode = oSelMode
        LoadImageList()
        LoadTreeView()
        SetCurrentAction()
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub SetCurrentAction()
        Dim oAction As Actions = Actions.GrupsDeResposta
        Select Case mSelMode
            Case SelModes.JustBrowse
                oAction = Actions.GrupsDeResposta
            Case SelModes.Missatges
                oAction = Actions.Missatges
            Case SelModes.Timbres
                oAction = Actions.Timbres
        End Select
        TreeView1.SelectedNode = TreeView1.Nodes(CInt(oAction))
    End Sub

    Private Sub LoadImageList()
        With ImageList1.Images
            .Clear()
            .Add(My.Resources.clip)
            .Add(My.Resources.tel)
            .Add(My.Resources.People_Blue)
            .Add(My.Resources.SandClock)
            .Add(My.Resources.wav)
            .Add(My.Resources.bell)
        End With
    End Sub

    Private Sub LoadTreeView()
        For Each v As Integer In [Enum].GetValues(GetType(Actions))
            TreeView1.Nodes.Add(GetNode(v))
        Next
    End Sub

    Private Function GetNode(ByVal v As Actions) As MaxiSrvr.TreeNodeObj
        Dim oNode As New maxisrvr.TreeNodeObj(, , v)
        With oNode
            .ImageIndex = CInt(v)
            .SelectedImageIndex = CInt(v)
        End With

        Select Case v
            Case Actions.GrupsDeResposta
                oNode.Text = "Grups de resposta"
            Case Actions.Linies
                oNode.Text = "Linies de entrada"
            Case Actions.Usuaris
                oNode.Text = "Usuaris"
            Case Actions.Festius
                oNode.Text = "Festius"
            Case Actions.Missatges
                oNode.Text = "Missatges contestador"
            Case Actions.Timbres
                oNode.Text = "Timbres"
        End Select
        Return oNode
    End Function

    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Function CurrentAction() As Actions
        Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        Dim oAction As Actions = CType(oNode.Cod, Actions)
        Return oAction
    End Function

    Private Sub LoadGrid()
        ListView1.Items.Clear()
        ListView1.SmallImageList = ImageList1
        mObjectArrayList = New ArrayList
        Dim iAction As Actions = CInt(CurrentAction())

        Select Case CurrentAction()
            Case Actions.GrupsDeResposta
                ListView1.View = View.Tile
                For Each oAgrupacio As TelGrupDeResposta In TelGrupsDeResposta.Actives
                    ListView1.Items.Add(oAgrupacio.Nom, iAction)
                    mObjectArrayList.Add(oAgrupacio)
                Next
            Case Actions.Linies
                ListView1.SmallImageList = ImageListLinies
                ListView1.View = View.Tile
                Dim oIcon As LiniaIcons
                For Each oLinia As TelLinia In TelLinies.Actives
                    If oLinia.Baixa > Date.MinValue Then
                        oIcon = LiniaIcons.Obsoleta
                    ElseIf oLinia.Privat Then
                        oIcon = LiniaIcons.Privada
                    ElseIf oLinia.GrupDeResposta Is Nothing Then
                        oIcon = LiniaIcons.Externa
                    Else
                        oIcon = LiniaIcons.Centraleta
                    End If
                    ListView1.Items.Add(oLinia.Id, CInt(oIcon))
                    mObjectArrayList.Add(oLinia)
                Next
            Case Actions.Usuaris
                ListView1.View = View.List
                For Each oUsr As Usr In Usrs.EnabledForCommunications
                    ListView1.Items.Add(oUsr.login, iAction)
                    mObjectArrayList.Add(oUsr)
                Next
            Case Actions.Festius
                For Each oFestiu As TelFestiu In TelFestius.all(True)
                    ListView1.Items.Add(Format(oFestiu.FchFrom, "dd/MM") & " " & oFestiu.Nom, iAction)
                    mObjectArrayList.Add(oFestiu)
                Next
                ListView1.View = View.List
            Case Actions.Missatges
                For Each oMissatge As TelMissatge In TelMissatges.AllMissatges(True)
                    ListView1.Items.Add(oMissatge.Nom, iAction)
                    mObjectArrayList.Add(oMissatge)
                Next
                ListView1.View = View.Tile
            Case Actions.Timbres
                For Each oTimbre As TelTimbre In TelTimbres.AllTimbres()
                    ListView1.Items.Add(oTimbre.Nom, iAction)
                    mObjectArrayList.Add(oTimbre)
                Next
                ListView1.View = View.Tile
        End Select
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        If CurrentObject() IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf DoZoom))
        End If
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        ListView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Function CurrentObject() As Object
        Dim retVal As Object = Nothing
        If ListView1.SelectedIndices.Count > 0 Then
            Dim iSelectedIndex As Integer = ListView1.SelectedIndices(0)
            retVal = mObjectArrayList(iSelectedIndex)
        End If
        Return retVal
    End Function

    Private Sub DoZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItm As ListViewItem = ListView1.SelectedItems(0)
        Select Case CurrentAction()
            Case Actions.GrupsDeResposta
                Dim oFrm As New Frm_TelGrupDeResposta(CurrentObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Linies
                Dim oFrm As New Frm_TelLinia(CurrentObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Usuaris
                Dim oUsr As Usr = CType(CurrentObject(), Usr)
                Dim oContact As Contact = MaxiSrvr.Contact.FromUsr(BLL.BLLApp.Emp, oUsr)
                Dim oFrm As New Frm_Contact2(oContact)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Festius
                Dim oFrm As New Frm_TelFestiu(CurrentObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Missatges
                Dim oFrm As New Frm_TelMissatge(CurrentObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Timbres
                Dim oFrm As New Frm_TelTimbre(CurrentObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
        End Select
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case CurrentAction()
            Case Actions.GrupsDeResposta
                Dim NewObject As New TelGrupDeResposta()
                NewObject.Nom = "(nom del nou Grup de Resposta)"
                Dim oFrm As New Frm_TelGrupDeResposta(NewObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Linies
                Dim NewObject As New TelLinia("")
                Dim oFrm As New Frm_TelLinia(NewObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Usuaris
            Case Actions.Festius
                Dim NewObject As TelFestiu = TelFestiu.NewFestiu
                Dim oFrm As New Frm_TelFestiu(NewObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Missatges
                Dim NewObject As New TelMissatge
                NewObject.Nom = "(nom del nou missatge de contestador)"
                Dim oFrm As New Frm_TelMissatge(NewObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
            Case Actions.Timbres
                Dim NewObject As New TelTimbre
                NewObject.Nom = "(nom del nou timbre)"
                Dim oFrm As New Frm_TelTimbre(NewObject)
                AddHandler oFrm.AfterUpdate, AddressOf LoadGrid
                oFrm.Show()
        End Select
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Dim BlZoom As Boolean = (mSelMode = SelModes.JustBrowse)

        If Not BlZoom Then
            Select Case CurrentAction()
                Case Actions.Missatges
                    BlZoom = (mSelMode <> SelModes.Missatges)
                Case Actions.Timbres
                    BlZoom = (mSelMode <> SelModes.Timbres)
            End Select
        End If

        If BlZoom Then
            DoZoom(Nothing, EventArgs.Empty)
        Else
            RaiseEvent AfterSelect(CurrentObject, EventArgs.Empty)
            Me.Close()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class