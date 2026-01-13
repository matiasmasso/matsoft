
Public Class Frm_FraRep
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
    Friend WithEvents Xl_AmtCur1 As Xl_AmountCur
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LabelDsc As System.Windows.Forms.Label
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Xl_AmtCur1 = New Xl_AmountCur
        Me.Label2 = New System.Windows.Forms.Label
        Me.LabelDsc = New System.Windows.Forms.Label
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Xl_AmtCur1
        '
        Me.Xl_AmtCur1.Amt = Nothing
        Me.Xl_AmtCur1.Location = New System.Drawing.Point(126, 161)
        Me.Xl_AmtCur1.Name = "Xl_AmtCur1"
        Me.Xl_AmtCur1.Size = New System.Drawing.Size(168, 20)
        Me.Xl_AmtCur1.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(62, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "comisió:"
        '
        'LabelDsc
        '
        Me.LabelDsc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelDsc.Location = New System.Drawing.Point(6, 9)
        Me.LabelDsc.Name = "LabelDsc"
        Me.LabelDsc.Size = New System.Drawing.Size(288, 144)
        Me.LabelDsc.TabIndex = 10
        '
        'ButtonDel
        '
        Me.ButtonDel.Location = New System.Drawing.Point(-2, 233)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(88, 24)
        Me.ButtonDel.TabIndex = 9
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(118, 233)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 24)
        Me.ButtonCancel.TabIndex = 8
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(206, 233)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(88, 24)
        Me.ButtonOk.TabIndex = 7
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'Frm_FraRep
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(296, 266)
        Me.Controls.Add(Me.Xl_AmtCur1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LabelDsc)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Name = "Frm_FraRep"
        Me.Text = "Frm_FraRep"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private mRep As Rep
    Private mFra As Fra
    Private mCancel As Boolean = True

    Public WriteOnly Property Rep() As Rep
        Set(ByVal Value As Rep)
            mRep = Value
        End Set
    End Property

    Public WriteOnly Property Fra() As Fra
        Set(ByVal Value As Fra)
            mFra = Value
        End Set
    End Property

    Public ReadOnly Property Cancel() As Boolean
        Get
            Return mCancel
        End Get
    End Property

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la comisió?", MsgBoxStyle.OKCancel, "FRA." & mFra.Id)
        If rc = MsgBoxResult.Ok Then
            MsgBox("cal refer el programa!", MsgBoxStyle.Exclamation)
            'RemoveRep()
            'mCancel = False
            'Me.Close()
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub


    Private Sub Xl_AmtCur1_AfterUpdateValue(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Xl_AmtCur1.AfterUpdateValue
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

    End Sub

    Private Sub Frm_FraRep_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
      
    End Sub
End Class

