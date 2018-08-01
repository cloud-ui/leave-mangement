<template>
    <div class="index">
        <div class="index-title">
            <p>公司部门</p>
        </div>
        <div class="index-body">
            <div class="index-body-title"> 
                <div style="display:flex;">
                    <p>共 <span>{{totalCount}}</span> 个部门</p>
                    <el-button style="padding:0px 0px 0px 10px;" @click="handleAdd()" type="text" icon="el-icon-plus">添加部门</el-button>
                </div>
                <el-input style="width:30%" placeholder="请输入内容" v-model="query" class="input-with-select">
                <el-button slot="append" @click="handleChangeQuery()">搜索</el-button></el-input>
            </div>
            <el-table :data="tableData" border stripe 
            :highlight-current-row="true"
            :header-cell-style="headerCellStyle"
            style="width: 100%">
             <el-table-column
                prop="code"
                label="部门编码"
                align="center"
                width="100">
             </el-table-column>
             <el-table-column
                prop="name"
                align="center"
                label="名称">
             </el-table-column>
             <el-table-column
                prop="manger"
                width="200"
                align="center"
                label="经理">
             </el-table-column>
             <el-table-column
                prop="workerCount"
                width="100"
                align="center"
                label="部门人数">
                <template slot-scope="scope">
                    <router-link class="link" :to="`/worker/`+scope.row.id">{{scope.row.workerCount}}</router-link>
                </template>
             </el-table-column>
             <el-table-column
                label="操作"
                align="center"
                width="100">
                <template slot-scope="scope">
                    <el-dropdown>
                        <i class="el-icon-menu"></i>
                        <el-dropdown-menu slot="dropdown">
                            <el-dropdown-item>
                             <el-button @click="handleEdit(scope.row)" type="text" size="small" icon="el-icon-edit">编辑</el-button>
                            </el-dropdown-item>
                            <el-dropdown-item>
                             <el-button @click="handleDelete(scope.row)" style="color:red;" type="text" size="small" icon="el-icon-delete">删除</el-button>
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
        <el-dialog :visible.sync="dialogVisible" :before-close="handleClose">
         <comp-adddep v-if="formInfo.type === 'add'" @closeForm='handleClose' @close='closeForm' ref="compForm"></comp-adddep>
         <comp-singledep v-if="formInfo.type === 'edit'" :formInfo="formInfo" :close="closeForm" @close='closeForm' ref="editForm"></comp-singledep>
        </el-dialog>
    </div>
</template>
<script>
import '../dangan.scss'
import '../../index.scss'
import CompAdddep from './adddep'
import CompSingledep from './singledep'
import {FileApi} from '../api.js'
export default {
    data(){
        return{
            tableData:[],
            totalCount:0,
            query:'',
            currentPage:1,
            pageSize:20,
            dialogVisible:false,
            headerCellStyle: {
                backgroundColor: '#f2f2f2',
                fontSize: '14px',
                color: '#434343',
                fontWeight: 400
            },
            formInfo:{
                type:'add',
                formData:{}
            }
        }
    },
    components:{
        CompAdddep,
        CompSingledep
    },
    mounted(){
        this.loadData()
    },
    methods:{
        handleSizeChange(val) {
            this.pageSize = val;
            this.loadData();
      },
      handleCurrentChange(val) {
          this.currentPage = val;
          this.loadData();
      },
      handleChangeQuery(){
          this.loadData()
      },
      //点击删除
      handleDelete(row){
          this.$confirm('此操作将永久删除该部门, 是否继续?', '提示', {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning'
                    }).then(() => {
                        if(row.workerCount !=0){
                            this.$message({
                            type: 'error',
                            message: '您要删除的部门有员工，请进行调整后再删除！'
                        });
                        }else{
                            FileApi.deleteDeparment(row.id).then(res => {
                            this.loadData()
                            var type1 = ''
                            if(res.data.isSuccess){
                                type1 = 'success'
                            }else{
                                type1 = 'error'
                            }
                            this.$message({
                                type: type1,
                                message: res.data.message
                            });
                        })
                        }
                    }).catch(() => {
                        this.$message({
                            type: 'info',
                            message: '已取消删除'
                        });
                    });
      },
      //点击编辑
      handleEdit(row){
          this.dialogVisible = true
          this.formInfo.type = 'edit'
          this.formInfo.formData = row
      },
      //点击添加
      handleAdd(){
          this.dialogVisible = true
          this.formInfo.type = 'add'
      },
      loadData(){
          const params = {
              currentPage:this.currentPage,
              currentPageSize:this.pageSize,
              query:this.query
          };
          FileApi.getDeparmentList(params).then(res=>{
              this.totalCount = res.data.totalCount;
              this.tableData = res.data.data;
          });
      },
      //关闭弹窗
      handleClose(){
          this.dialogVisible = false
      }, 
      closeForm(val){
          this.loadData()
          this.dialogVisible = val
      }
    }
}
</script>
<style>
.el-dialog__body {
    padding: 10px 20px;
    color: #606266;
    font-size: 14px;
}
</style>

