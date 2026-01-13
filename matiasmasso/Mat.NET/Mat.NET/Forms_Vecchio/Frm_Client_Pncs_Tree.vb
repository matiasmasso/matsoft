Public Class Frm_Client_Pncs_Tree
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(292, 273)
        Me.TreeView1.TabIndex = 0
        '
        'Frm_Client_Pncs_Tree
        '
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.TreeView1)
        Me.Name = "Frm_Client_Pncs_Tree"
        Me.Text = "Frm_Client_Pncs_Tree"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mClient As Client
    Private mEmp as DTOEmp

    Public WriteOnly Property Client() As Client
        Set(ByVal Value As Client)
            If Not Value Is Nothing Then
                mClient = Value
                mEmp = mClient.Emp
                Me.Text = "PENDENTS D'ENTREGA " & mClient.Clx
                LoadTree()
            End If
        End Set
    End Property

    Private Sub LoadTree()
        Dim SQL As String = "SELECT TPA.TPA, TPA.DSC AS tpadsc, STP.stp, STP.dsc AS stpdsc, PNC.ArtGuid, ART.ORD, PNC.Pn2, PNC.PdcGuid, Pdc.pdc, PDC.FCH, PNC.LIN " _
        & "FROM PDC INNER JOIN " _
        & "PNC ON PDC.Guid = PNC.PdcGuid INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
        & "STP ON ART.Category = STP.Guid INNER JOIN " _
        & "TPA ON STP.Brand = TPA.Guid " _
        & "WHERE PDC.cli = @CliGuid AND " _
        & "PNC.Pn2 <> 0 AND " _
        & "PNC.Cod =" & DTOPurchaseOrder.Codis.client & " " _
        & "ORDER BY TPA.ORD, STP.ord, ART.ord, PDC.fch, PDC.pdc"
        Dim oTpa As New Tpa(mEmp)
        Dim oStp As New Stp(oTpa)
        Dim oArt As New Art(oStp)
        Dim oPdc As Pdc = Nothing
        Dim oPnc As LineItmPnc = Nothing
        Dim NodeText As String
        Dim oNodeTpa As MaxiSrvr.TreeNodeObj = Nothing
        Dim oNodeStp As MaxiSrvr.TreeNodeObj = Nothing
        Dim oNodeArt As MaxiSrvr.TreeNodeObj = Nothing
        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@CliGuid", mClient.Guid.ToString)
        Do While oDrd.Read
            If oDrd("TPA") <> oTpa.Id Then
                oTpa = Tpa.FromNum(mEmp, CInt(oDrd("TPA")))
                oNodeTpa = New MaxiSrvr.TreeNodeObj(oDrd("TPADSC"), oTpa)
                TreeView1.Nodes.Add(oNodeTpa)
                oNodeTpa.Expand()
            End If
            If oDrd("STP") <> oStp.Id Then
                oStp = New Stp(oTpa, oDrd("STP"))
                oNodeStp = New MaxiSrvr.TreeNodeObj(oDrd("STPDSC"), oStp)
                oNodeTpa.Nodes.Add(oNodeStp)
                oNodeStp.Expand()
            End If

            oArt = New Art(CType(oDrd("ArtGuid"), Guid))
            oPdc = New Pdc(CType(oDrd("PdcGuid"), Guid))
            oPnc = New LineItmPnc(oPdc, oDrd("LIN"))
            oPnc.SetItm()
            oPnc.Art = oArt
            NodeText = oDrd("PN2") & " " & oDrd("ORD") & " " & oDrd("FCH")
            oNodeArt = New MaxiSrvr.TreeNodeObj(NodeText, oPnc)
            oNodeStp.Nodes.Add(oNodeArt)
        Loop
    End Sub


    Private Sub ShowPncs(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowClientPncs(mClient)
    End Sub
End Class
