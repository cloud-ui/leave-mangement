<template>
    <el-card style="width:82%" class="box-card">
        <div class="index-body-title">
            <p>共 <span>{{tableData.length}}</span> 个职位</p>
            <el-button @click="addPosition()" type="primary" size="mini">添加</el-button>
        </div>
        <comp-table :tableData="tableData" :tableHeader="tableHeader" :tableAttr="tableAttr" :headerCellStyle="headerCellStyle" className="tableClassName" @tableOtherClick="tableOtherClick"></comp-table>
        <el-dialog :title="formTitle" :visible.sync="dialogVisible" width="408px" :before-close="handleClose">
            <comp-form v-if="!treeShow" @closeForm='handleClose' @close='closeForm' ref="compForm" :formInfo="formInfo"></comp-form>
            <comp-tree @closeForm='handleClose' @close='closeForm' @positionId="positionId"></comp-tree>
        </el-dialog>
    </el-card>
</template>
<script>
    import '../dangan.scss';
    import {
        FileApi
    } from '../api.js'
    import CompTable from '../../../packages/components/table'
    import compForm from './addForm'
    import CompTree from './permissiontree'
    export default {
        components: {
            CompTable,
            compForm,
            CompTree
        },
        data() {
            return {
                formTitle: '',
                dialogVisible: false,
                treeShow:false,
                positionId:'',
                formInfo: {
                    state: 'position',
                    type: '',
                    data: {},
                },
                tableAttr: {
                    noIndex: false,
                    other: [{
                            name: '编辑',
                            style: 'text',
                            type: 'edit',
                            icon: 'el-icon-edit',
                            color: '#409eff'
                        },
                        {
                            name: '删除',
                            style: 'text',
                            type: 'delete',
                            icon: 'el-icon-delete',
                            color: '#f56c6c'
                        },{
                            name:'权限配置',
                            style:'text',
                            type:'permission',
                            icon:'el-icon-refresh',
                            color:'#5fb878'
                        }
                    ]
                },
                tableHeader: [{
                    prop: 'id',
                    label: '序号',
                    width: 80,
                }, {
                    prop: 'name',
                    label: '职位名称',
                }, {
                    prop: 'pay',
                    label: '薪资(/月)',
                    width: 180
                }],
                tableData: [],
                headerCellStyle: {
                    backgroundColor: '#f2f2f2',
                    fontSize: '14px',
                    color: '#434343',
                    fontWeight: 400
                },
                query: ''
            }
        },
        mounted() {
            this.loadData()
        },
        methods: {
            loadData() {
                FileApi.getPositionList().then(res => {
                    this.tableData = res.data
                })
            },
            tableOtherClick(row, type, index) {
                console.log(row)
                if (type === 'edit') {
                    this.formTitle = "编辑职位"
                    this.formInfo.type = "edit"
                    this.formInfo.data = {
                        ...row
                    }
                    this.dialogVisible = true
                    this.treeShow = false
                }
                if (type === 'delete') {
                    this.$confirm('此操作将永久删除该职位, 是否继续?', '提示', {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning'
                    }).then(() => {
                        FileApi.deletePosition(row.id).then(res => {
                            this.loadData()
                            var type1 = res.data.isSuccess?'success': 'error'
                            this.$message({
                                type: type1,
                                message: res.data.message
                            });
                        })
                    }).catch(() => {
                        this.$message({
                            type: 'info',
                            message: '已取消删除'
                        });
                    });
                }
                if(type === 'permission'){
                    this.treeShow = true
                    this.formTitle = "权限配置"
                    this.positionId = row.id
                    this.dialogVisible = true
                }
            },
            //点击添加按钮
            addPosition() {
                this.formTitle = "添加职位"
                this.formInfo.type = "add"
                this.formInfo.data = {}
                this.dialogVisible = true
            },
            //关闭弹框
            handleClose() {
                this.dialogVisible = false
                if(!this.treeShow){
                    this.$refs.compForm.resetForm('addForm');
                }
                
            },
            closeForm(val) {
                this.loadData()
                this.dialogVisible = val
                this.$refs.compForm.resetForm('addForm');
                if(!this.treeShow){
                    this.$refs.compForm.resetForm('addForm');
                }
            }
        }
    }
</script>

