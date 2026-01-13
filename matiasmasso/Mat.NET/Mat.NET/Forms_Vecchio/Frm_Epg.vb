

Public Class Frm_Epg
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonBal1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonBal2 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonAct2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAct1 As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label3 = New System.Windows.Forms.Label
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioButtonBal2 = New System.Windows.Forms.RadioButton
        Me.RadioButtonBal1 = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RadioButtonAct2 = New System.Windows.Forms.RadioButton
        Me.RadioButtonAct1 = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(32, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 16)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "nom:"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(104, 248)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonCancel.TabIndex = 15
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(192, 248)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(80, 24)
        Me.ButtonOk.TabIndex = 14
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(80, 136)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(192, 20)
        Me.TextBoxNom.TabIndex = 13
        Me.TextBoxNom.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonBal2)
        Me.GroupBox1.Controls.Add(Me.RadioButtonBal1)
        Me.GroupBox1.Location = New System.Drawing.Point(32, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 56)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        '
        'RadioButtonBal2
        '
        Me.RadioButtonBal2.Location = New System.Drawing.Point(16, 32)
        Me.RadioButtonBal2.Name = "RadioButtonBal2"
        Me.RadioButtonBal2.Size = New System.Drawing.Size(136, 16)
        Me.RadioButtonBal2.TabIndex = 1
        Me.RadioButtonBal2.Text = "Compte d'explotació"
        '
        'RadioButtonBal1
        '
        Me.RadioButtonBal1.Location = New System.Drawing.Point(16, 16)
        Me.RadioButtonBal1.Name = "RadioButtonBal1"
        Me.RadioButtonBal1.Size = New System.Drawing.Size(96, 16)
        Me.RadioButtonBal1.TabIndex = 0
        Me.RadioButtonBal1.Text = "Balanç"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButtonAct2)
        Me.GroupBox2.Controls.Add(Me.RadioButtonAct1)
        Me.GroupBox2.Location = New System.Drawing.Point(32, 72)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(240, 56)
        Me.GroupBox2.TabIndex = 19
        Me.GroupBox2.TabStop = False
        '
        'RadioButtonAct2
        '
        Me.RadioButtonAct2.Location = New System.Drawing.Point(16, 32)
        Me.RadioButtonAct2.Name = "RadioButtonAct2"
        Me.RadioButtonAct2.Size = New System.Drawing.Size(136, 16)
        Me.RadioButtonAct2.TabIndex = 1
        Me.RadioButtonAct2.Text = "Passiu"
        '
        'RadioButtonAct1
        '
        Me.RadioButtonAct1.Location = New System.Drawing.Point(16, 16)
        Me.RadioButtonAct1.Name = "RadioButtonAct1"
        Me.RadioButtonAct1.Size = New System.Drawing.Size(96, 16)
        Me.RadioButtonAct1.TabIndex = 0
        Me.RadioButtonAct1.Text = "Actiu"
        '
        'Frm_Epg
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Name = "Frm_Epg"
        Me.Text = "EPIGRAFE"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mEpg As Epg
    Private mAllowEvents As Boolean

    Public WriteOnly Property Epg() As Epg
        Set(ByVal Value As Epg)
            mEpg = Value
            Me.Text = Me.Text & " " & mEpg.Id
            RadioButtonBal1.Checked = (mEpg.Bal = pgcgrup.BalCods.balanç)
            RadioButtonBal2.Checked = (mEpg.Bal = pgcgrup.BalCods.explotacio)
            With RadioButtonAct1
                .Checked = (mEpg.Act = PgcCta.Acts.deutora)
                .Text = MaxiSrvr.Epg.ActNom(CurrentBal, PgcCta.Acts.deutora)
            End With
            With RadioButtonAct2
                .Checked = (mEpg.Act = PgcCta.Acts.creditora)
                .Text = MaxiSrvr.Epg.ActNom(CurrentBal, PgcCta.Acts.creditora)
            End With
            TextBoxNom.Text = mEpg.Nom
            mAllowEvents = True
        End Set
    End Property

    Private Function CurrentAct() As PgcCta.Acts
        If RadioButtonAct1.Checked Then
            Return PgcCta.Acts.deutora
        Else
            Return PgcCta.Acts.creditora
        End If
    End Function

    Private Function CurrentBal() As PgcGrup.BalCods
        If RadioButtonBal1.Checked Then
            Return pgcgrup.BalCods.balanç
        Else
            Return pgcgrup.BalCods.explotacio
        End If
    End Function

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mEpg.Id = 0 Then
            mEpg = New Epg(CurrentBal, CurrentAct)
        End If
        With mEpg
            .Nom = TextBoxNom.Text
            .Update()
        End With
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class
