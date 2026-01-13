Public Class Frm_Provincia
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
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Pais1 As Xl_Pais
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_Regio1 As Xl_Regio
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxZip As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Xl_Pais1 = New Xl_Pais
        Me.Label3 = New System.Windows.Forms.Label
        Me.Xl_Regio1 = New Xl_Regio
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBoxZip = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'ButtonDel
        '
        Me.ButtonDel.Location = New System.Drawing.Point(8, 144)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(72, 24)
        Me.ButtonDel.TabIndex = 13
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(120, 144)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(208, 144)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(80, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(48, 56)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(232, 20)
        Me.TextBoxNom.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "nom:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Pais:"
        '
        'Xl_Pais1
        '
        Me.Xl_Pais1.Location = New System.Drawing.Point(48, 8)
        Me.Xl_Pais1.Name = "Xl_Pais1"
        Me.Xl_Pais1.Country = Nothing
        Me.Xl_Pais1.Size = New System.Drawing.Size(60, 15)
        Me.Xl_Pais1.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 16)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Regió:"
        '
        'Xl_Regio1
        '
        Me.Xl_Regio1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Regio1.Location = New System.Drawing.Point(48, 32)
        Me.Xl_Regio1.Name = "Xl_Regio1"
        Me.Xl_Regio1.Regio = Nothing
        Me.Xl_Regio1.Size = New System.Drawing.Size(232, 20)
        Me.Xl_Regio1.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 16)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "zip:"
        '
        'TextBoxZip
        '
        Me.TextBoxZip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxZip.Location = New System.Drawing.Point(48, 80)
        Me.TextBoxZip.Name = "TextBoxZip"
        Me.TextBoxZip.Size = New System.Drawing.Size(64, 20)
        Me.TextBoxZip.TabIndex = 17
        '
        'Frm_Provincia
        '
        Me.ClientSize = New System.Drawing.Size(292, 173)
        Me.Controls.Add(Me.TextBoxZip)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_Regio1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Pais1)
        Me.Name = "Frm_Provincia"
        Me.Text = "PROVINCIA"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mProvincia As MaxiSrvr.Provincia
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oProvincia As Provincia)
        MyBase.New()
        InitializeComponent()
        mProvincia = oProvincia
        Refresca()
    End Sub

    Public ReadOnly Property Provincia() As MaxiSrvr.Provincia
        Get
            Return mProvincia
        End Get
    End Property

    Private Sub Refresca()
        If Not mProvincia.Exists Then
            Me.Text = "NOVA PROVINCIA..."
        Else
            Me.Text = "PROVINCIA " & mProvincia.Nom
            'Xl_Pais1.Locked = True
            Xl_Regio1.Locked = True
            Xl_Regio1.Regio = mProvincia.Regio
            TextBoxZip.Text = mProvincia.Zip
            TextBoxNom.Text = mProvincia.Nom
        End If
        Xl_Pais1.Country = New DTOCountry(mProvincia.Country.Guid)
        mAllowEvents = True
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        If mAllowEvents Then ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mProvincia
            .Nom = TextBoxNom.Text
            .Regio = Xl_Regio1.Regio
            .Zip = TextBoxZip.Text
            .Update()
            RaiseEvent AfterUpdate(mProvincia, EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem la provincia " & mProvincia.Nom & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim iRc As Integer = mProvincia.Delete
            If iRc = vbOK Then
                MsgBox("Provincia " & mProvincia.Nom & " eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(mProvincia, EventArgs.Empty)
                Me.Close()
            Else
                MsgBox("Operació cancelada", MsgBoxStyle.Critical, "M+O")
            End If
        End If
    End Sub

    Private Sub TextBoxZip_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxZip.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_Regio1_AfterUpdate(ByVal oRegio As MaxiSrvr.Regio) Handles Xl_Regio1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub
End Class
