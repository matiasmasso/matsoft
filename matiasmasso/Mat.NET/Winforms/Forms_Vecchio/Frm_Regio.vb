Public Class Frm_Regio
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
    Friend WithEvents Xl_Pais1 As Xl_Pais
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Xl_Pais1 = New Xl_Pais
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Xl_Pais1
        '
        Me.Xl_Pais1.Location = New System.Drawing.Point(40, 8)
        Me.Xl_Pais1.Name = "Xl_Pais1"
        Me.Xl_Pais1.Country = Nothing
        Me.Xl_Pais1.Size = New System.Drawing.Size(60, 15)
        Me.Xl_Pais1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Pais:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "nom:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxNom.Location = New System.Drawing.Point(40, 32)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(248, 20)
        Me.TextBoxNom.TabIndex = 3
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(208, 64)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(80, 24)
        Me.ButtonOk.TabIndex = 4
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(120, 64)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonCancel.TabIndex = 5
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonDel
        '
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(8, 64)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonDel.TabIndex = 6
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'Frm_Regio
        '
        Me.ClientSize = New System.Drawing.Size(292, 93)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Pais1)
        Me.Name = "Frm_Regio"
        Me.Text = "REGIO"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mRegio As MaxiSrvr.Regio
    Private mAllowEvents As Boolean

    Public Property Regio() As MaxiSrvr.Regio
        Get
            Return mRegio
        End Get
        Set(ByVal Value As MaxiSrvr.Regio)
            mRegio = Value
            If mRegio IsNot Nothing Then
                Me.Text = "NOVA REGIO..."
            Else
                Me.Text = "REGIO " & mRegio.Nom
                ButtonDel.Enabled = mRegio.AllowDelete
            End If
            'Xl_Pais1.Locked = True
            Xl_Pais1.Country = New DTOCountry(mRegio.Country.Guid)
            TextBoxNom.Text = mRegio.Nom
            mAllowEvents = True
        End Set
    End Property


    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        If mAllowEvents Then ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mRegio
            .Nom = TextBoxNom.Text
            .Update()
            Me.Close()
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem la regió " & mRegio.Nom & "?", MsgBoxStyle.OKCancel, "M+O")
        If rc = MsgBoxResult.OK Then
            Dim iRc As Integer = mRegio.Delete
            If iRc = vbOK Then
                MsgBox("Regio " & mRegio.Nom & " eliminada", MsgBoxStyle.Information, "M+O")
            Else
                MsgBox("Operació cancelada", MsgBoxStyle.Critical, "M+O")
            End If
        End If
    End Sub
End Class
