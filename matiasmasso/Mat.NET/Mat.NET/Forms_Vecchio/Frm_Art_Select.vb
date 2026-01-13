

Public Class Frm_Art_Select
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
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(504, 20)
        Me.TextBox1.TabIndex = 0
        '
        'Frm_Art_Select
        '
        Me.ClientSize = New System.Drawing.Size(504, 22)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Frm_Art_Select"
        Me.Text = "ARTICLE"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mArt As Art

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim sKey As String = TextBox1.Text
            If Not mArt Is Nothing Then
                If sKey = mArt.Nom_ESP Then
                    root.ShowArt(mArt)
                    Exit Sub
                End If
            End If

            If sKey = "" Then
                mArt = Nothing
            Else
                Dim oMgz As DTOMgz = BLL.BLLApp.Mgz
                Dim oSku As ProductSku = Finder.FindSku(oMgz, sKey)
                If oSku Is Nothing Then
                    mArt = Nothing
                Else
                    mArt = New Art(oSku.Guid)
                End If
            End If
            RefreshRequest(mArt, e)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mArt = CType(sender, Art)
        If mArt IsNot Nothing Then
            TextBox1.Text = mArt.Nom_ESP
            Me.Text = "ARTICLE " & mArt.Id
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If mArt IsNot Nothing Then
            Dim oMenu_Art As New Menu_Art(mArt)
            AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)

            oContextMenu.Items.Add("-")
        End If

        TextBox1.ContextMenuStrip = oContextMenu
    End Sub


End Class
