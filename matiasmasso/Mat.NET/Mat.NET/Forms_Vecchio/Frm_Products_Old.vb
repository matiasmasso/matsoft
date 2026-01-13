

Public Class Frm_Products_Old
    Private mProduct As Product
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private mAllowEvents As Boolean = False

    Private Enum Levels
        NotSet
        Tpa
        Stp
        Ctg
        Art
    End Enum

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oProduct As Product)
        MyBase.new()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mProduct = oProduct

        LoadTpas()
    End Sub

    Public ReadOnly Property Product() As Product
        Get
            Return mProduct
        End Get
    End Property

    Private Sub LoadTpas()
        Dim SQL As String = "SELECT TPA,DSC FROM TPA WHERE EMP=@EMP "
        If CheckBoxHideObsoletos.Checked Then
            SQL = SQL & "AND OBSOLETO=0 "
        End If
        SQL = SQL & "ORDER BY ORD"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id)

        mAllowEvents = False
        With ListBox1
            .DataSource = oDs.Tables(0)
            .ValueMember = "TPA"
            .DisplayMember = "DSC"
        End With
        mAllowEvents = True

    End Sub

    Private Sub LoadStps()
        Dim SQL As String = "SELECT STP,DSC FROM STP WHERE EMP=@EMP AND TPA=@TPA "
        If CheckBoxHideObsoletos.Checked Then
            SQL = SQL & "AND OBSOLETO=0 "
        End If
        SQL = SQL & "ORDER BY ORD"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@TPA", CurrentTpa.Id)

        mAllowEvents = False
        With ListBox2
            .DataSource = oDs.Tables(0)
            .ValueMember = "STP"
            .DisplayMember = "DSC"
        End With
        mAllowEvents = True
    End Sub

    Private Sub LoadArts()
        Dim SQL As String = "SELECT ART,ORD FROM ART WHERE EMP=@EMP AND TPA=@TPA AND STP=@STP "
        If CheckBoxHideObsoletos.Checked Then
            SQL = SQL & "AND OBSOLETO=0 "
        End If
        SQL = SQL & "ORDER BY ORD"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@TPA", CurrentTpa.Id, "@STP", CurrentStp.Id)

        mAllowEvents = False
        With ListBox3
            .DataSource = oDs.Tables(0)
            .ValueMember = "ART"
            .DisplayMember = "ORD"
        End With
        mAllowEvents = True
    End Sub

    Private Function CurrentTpa() As Tpa
        Dim TpaId As Integer = ListBox1.SelectedValue
        Dim oTpa As New Tpa(mEmp, TpaId)
        Return oTpa
    End Function

    Private Function CurrentStp() As Stp
        Dim StpId As Integer = ListBox2.SelectedValue
        Dim oStp As New Stp(CurrentTpa, StpId)
        Return oStp
    End Function

    Private Function CurrentArt() As Art
        Dim ArtId As Integer = ListBox3.SelectedValue
        Dim oArt As Art = MaxiSrvr.Art.FromNum(mEmp, ArtId)
        Return oArt
    End Function

    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        mProduct = New Product(CurrentTpa)
        RaiseEvent AfterSelect(mProduct, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If mAllowEvents Then
            LoadStps()
            LoadArts()
        End If
    End Sub

    Private Sub ListBox2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox2.DoubleClick
        mProduct = New Product(CurrentStp)
        RaiseEvent AfterSelect(mProduct, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        If mAllowEvents Then
            LoadArts()
        End If
    End Sub

    Private Sub ListBox3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox3.DoubleClick
        mProduct = New Product(CurrentArt)
        RaiseEvent AfterSelect(mProduct, EventArgs.Empty)
        Me.Close()
    End Sub



    Private Sub CheckBoxHideObsoletos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxHideObsoletos.CheckedChanged
        LoadTpas()
    End Sub
End Class