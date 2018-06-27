<template>
    <div class="index">
        <div class="index-title">
            <p>提交申请</p>
        </div>
        <div class="index-body">
            <div class="index-body-title">
                <div style="display:flex;">
                    <p>共 <span>{{totalCount}}</span> 条提交的申请</p>
                    <router-link to="/addApplication/-1" class="link"><i class="el-icon-plus"></i>添加申请</router-link>
                </div>
                <el-input style="width:30%" placeholder="请输入内容" v-model="query" class="input-with-select">
                    <el-button slot="append" @click="handleChangeQuery()">搜索</el-button>
                </el-input>
            </div>
            <el-table :data="tableData" :header-cell-style="headerCellStyle" border style="width: 100%">
                <el-table-column align="center" prop="id" label="序号" width="80">
                    <template slot-scope="scope">
                            {{scope.row.id}}
                    </template>
                </el-table-column>
                <el-table-column align="center" prop="workerName" label="员工名称" width="100">
                </el-table-column>
                <el-table-column align="center" prop="type" label="请假类型" width="120">
                </el-table-column>
                <el-table-column align="center" label="申请状态" width="100">
                    <template slot-scope="scope">
                        <p v-if="scope.row.state===1" class="approval-state unlook">{{scope.row.stateName}}</p>
                        <p v-if="scope.row.state===2" class="approval-state agree">{{scope.row.stateName}}</p>
                        <p v-if="scope.row.state===3" class="approval-state refuse">{{scope.row.stateName}}</p>
                    </template>
                </el-table-column>
                <el-table-column align="center" prop="startTime" label="开始时间">
                </el-table-column>
                <el-table-column align="center" prop="endTime" label="结束时间">
                </el-table-column>
                <el-table-column align="center" prop="createTime" label="创建时间">
                </el-table-column>
                <el-table-column align="center" label="操作" width="80">
                    <template slot-scope="scope">
                        <el-dropdown>
                            <i class="el-icon-menu"></i>
                            <el-dropdown-menu slot="dropdown">
                                <el-dropdown-item>
                                 <el-button type="text" size="small" @click="handleLook(scope.row.id)" icon="el-icon-view">查看</el-button>
                                </el-dropdown-item>
                            </el-dropdown-menu>
                        </el-dropdown>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination 
            style="padding-top:10px"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page="currentPage"
            :page-sizes="[100, 200, 300, 400]"
            :page-size="pageSize"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalCount">
            </el-pagination>
        </div>
        <el-dialog title="申请详情" :visible.sync="dialogVisible" :before-close="handleClose">
         <comp-look @closeForm='handleClose' @close='closeForm' :id="applicationId" ref="compForm"></comp-look>
        </el-dialog>
    </div>
</template>
<script>
    import '../index.scss'
    import './approval.scss'
    import CompLook from './applicationView'
    export default {
        components:{
            CompLook
        },
        data() {
            return {
                tableData: [{
                    id: 1,
                    workerName: '张三',
                    state:1,
                    stateName:'未审批',
                    type: '事前-病假',
                    startTime: '2018-06-08',
                    endTime: '2018-06-08',
                    createTime: '2018-02-06'
                },{
                    id: 2,
                    workerName: '张三',
                    state:2,
                    stateName:'未审批',
                    type: '事前-病假',
                    startTime: '2018-06-08',
                    endTime: '2018-06-08',
                    createTime: '2018-02-06'
                },{
                    id: 3,
                    workerName: '张三',
                    state:3,
                    stateName:'未审批',
                    type: '事前-病假',
                    startTime: '2018-06-08',
                    endTime: '2018-06-08',
                    createTime: '2018-02-06'
                }],
                headerCellStyle: {
                    backgroundColor: '#f2f2f2',
                    fontSize: '14px',
                    color: '#434343',
                    fontWeight: 400
                },
                currentPage: 1,
                pageSize: 20,
                query: '',
                totalCount: 0,
                dialogVisible: false,
                applicationId:0,
            }
        },
        methods: {
            loadData() {
            },
            handleSizeChange(val) {
                this.pageSize = val;
                this.loadData();
            },
            handleCurrentChange(val) {
                this.currentPage = val;
                this.loadData();
            },
            handleCommand(id,command){
                console.log(id)
                console.log(command)
            },
            //点击搜索
            handleChangeQuery() {
            },
            //点击查看
            handleLook(id){
                this.applicationId = id
                this.dialogVisible = true                
            },
            //关闭弹框
            handleClose(){
                this.dialogVisible = false
            },
            closeForm(val){
                this.dialogVisible = val
            }
        }
    }
</script>
<style lang="scss" scoped>
    .link {
        padding: 0px 0px 0px 10px;
        font-size: 14px;
        color: #409eff;
        text-decoration: none;
    }
</style>

