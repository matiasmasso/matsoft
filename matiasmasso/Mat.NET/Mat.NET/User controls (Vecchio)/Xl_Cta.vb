

Public Class Xl_Cta
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.ContextMenu = Me.ContextMenu1
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(136, 20)
        Me.TextBox1.TabIndex = 0
        '
        'ContextMenu1
        '
        '
        'Xl_Cta
        '
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Xl_Cta"
        Me.Size = New System.Drawing.Size(136, 28)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mCta As PgcCta
    Private mAllowEvents As Boolean = True

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Cta() As PgcCta
        Get
            Return mCta
        End Get
        Set(ByVal Value As PgcCta)
            If Not Value Is Nothing Then
                mAllowEvents = False
                mCta = Value
                TextBox1.Text = mCta.FullNom
                mAllowEvents = True
            End If
        End Set
    End Property


    Private Sub Xl_Cta_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        TextBox1.Width = MyBase.Width
        MyBase.Height = TextBox1.Height
    End Sub

    Private Sub ContextMenu1_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenu1.Popup
        'Dim oMenuItem As MenuItem
        If Not mCta Is Nothing Then
            With ContextMenu1.MenuItems
                .Clear()
                'For Each oMenuItem In New MenuItems_Cta(mCta)
                ' .Add(oMenuItem.CloneMenu)
                'Next
            End With
        End If
    End Sub

    Private Sub TextBox1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Validated
        If mAllowEvents Then
            Dim BlUpdate As Boolean
            If mCta Is Nothing Then
                BlUpdate = True
            Else
                If TextBox1.Text <> mCta.FullNom Then BlUpdate = True
            End If

            If BlUpdate Then
                Dim oPlan As PgcPlan = PgcPlan.FromToday
                Dim oCta As PgcCta = Finder.FindCta(oPlan, TextBox1.Text)
                If Not oCta Is Nothing Then
                    mCta = oCta
                    RaiseEvent AfterUpdate(oCta, New System.EventArgs)
                End If
                mAllowEvents = False
                TextBox1.Text = mCta.FullNom
                mAllowEvents = True

            End If
        End If
    End Sub

    Public Function ValidateFch(DtFch As Date) As Boolean
        Dim retval As Boolean
        If DtFch.Year >= mCta.Plan.YearFrom Then
            If mCta.Plan.YearTo = 0 Then
                retval = True
            Else
                If DtFch.Year < mCta.Plan.YearTo Then
                    retval = True
                End If
            End If
        End If

        If retval Then
            TextBox1.BackColor = Color.AliceBlue
        Else
            TextBox1.BackColor = Color.LightSalmon
        End If
        Return retval
    End Function
End Class
