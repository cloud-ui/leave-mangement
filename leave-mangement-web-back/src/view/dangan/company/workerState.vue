<template>
    <el-card style="width:82%" class="box-card">
        <div class="index-body-title">
            <p>共 <span>{{tableData.length}}</span> 个状态</p>
            <el-button @click="addState()" type="primary" size="mini">添加</el-button>
        </div>
        <comp-table :tableData="tableData" :tableHeader="tableHeader" :tableAttr="tableAttr" :headerCellStyle="headerCellStyle" className="tableClassName" @tableOtherClick="tableOtherClick"></comp-table>
        <el-dialog :title="formTitle" :visible.sync="dialogVisible" width="408px" :before-close="handleClose">
            <comp-form @closeForm='handleClose' @close='closeForm' ref="compForm" :formInfo="formInfo"></comp-form>
        </el-dialog>
    </el-card>
</template>
<script>
    import '../dangan.scss';
    import {FileApi} from '../api.js'
    import CompTable from '../../../packages/components/table'
    import compForm from './addForm'
    export default {
        components: {
            CompTable,
            compForm
        },
        data() {
            return {
                formTitle: '',
                dialogVisible: false,
                formInfo: {
                    state: 'state',
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
                        // {
                        //     name: '删除',
                        //     style: 'text',
                        //     type: 'delete',
                        //     icon: 'el-icon-delete',
                        //     color: '#f56c6c'
                        // },
                    ]
                },
                tableHeader: [{
                    prop: 'id',
                    label: '序号',
                    width: 80,
                }, {
                    prop: 'name',
                    label: '状态名称',
                }, {
                    prop: 'pay',
                    label: '薪资',
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
        mounted(){
            this.loadData()
        },
        methods: {
            loadData(){
                FileApi.getStateList().then(res=>{
                    this.tableData = res.data
                })
            },
            tableOtherClick(row, type, index) {
                if (type === 'edit') {
                    this.formTitle = "编辑状态"
                    this.formInfo.type = "edit"
                    this.formInfo.data = {
                        ...row
                    }
                    this.dialogVisible = true
                }
                if (type === 'delete') {
                    this.$confirm('此操作将永久删除该员工状态, 是否继续?', '提示', {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning'
                    }).then(() => {
                        FileApi.deleteState(row.id).then(res => {
                            this.loadData()
                            const type1 = res.data.isSuccess?'success':'error'
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
            },
            //点击添加按钮
            addState() {
                this.formTitle = "添加状态"
                this.formInfo.type = "add"
                this.formInfo.data = {}
                this.dialogVisible = true
            },
            //关闭弹框
            handleClose() {
                this.dialogVisible = false
                this.$refs.compForm.resetForm('addForm');
            },
            closeForm(val) {
                this.dialogVisible = val
                this.$refs.compForm.resetForm('addForm');
                this.loadData()
            }
        }
    }
</script>

