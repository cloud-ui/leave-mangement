<template>
    <div>
        <div class="add-tip">
            <ul>
                <li>1.批量上传文件，需下载文件模板</li>
                <li>2.下载模板填写内容</li>
                <li>3.上传文件</li>
                <li>4.上传失败的数据，请修改后将失败数据重新上传</li>
                <li>5.可能失败原因：公司不存在、经理不存在、部门已经存在</li>
            </ul>
        </div>
        <div class="row-item">
            <div class="title">数据模板下载：</div>
            <div class="link">
              <a :class="linkA" href="http://localhost:57503/api/File/DownloadFile">点击下载模板</a>
            </div>
        </div>
        <div class="row-item">
            <div class="title">选择数据文件：</div>
            <div class="upload">
              <el-upload
                class="upload-demo"
                ref="upload"
                action="https://jsonplaceholder.typicode.com/posts/"
                accept=".xlsx"
                :on-change="handleChangeFile"
                :file-list="fileList"
                :show-file-list="false"
                :auto-upload="false">
                <el-button class="select-file-button" slot="trigger" plain >{{selectFile}}</el-button>
                <i class="el-icon-circle-close clear-file-button" v-show="clearFileIsShow" @click="clearFile"></i>
              <el-button class="import-data-button" plain @click="importData">导入数据</el-button>
              </el-upload>
            </div>
        </div>
        <div v-if="showResult" class="show-result">
            <p class="show-result-tip">总共导入 <span>{{successCount + badCount}}</span> 条数据，
            成功 <span class="show-result-tip-s">{{successCount}}</span> 条，失败 <span class="show-result-tip-b">{{badCount}}</span> 条
            </p>
            <div>
                <el-table
    :data="result"
    height="250"
    style="width: 100%">
    <el-table-column
      prop="company"
      label="公司名称"
      width="150">
    </el-table-column>
    <el-table-column
      prop="manager"
      align="center"
      label="部门经理"
      width="90">
    </el-table-column>
    <el-table-column
      prop="name"
      align="center"
      label="部门名称">
    </el-table-column>
    <el-table-column
      prop="workerCount"
      align="center"
      label="部门人数">
    </el-table-column>
    <el-table-column
      prop="code"
      align="center"
      label="部门代码">
    </el-table-column>
    <el-table-column
      prop="isSuccess"
      align="center"
      label="是否成功">
      <template slot-scope="scope">
          <i :class="scope.row.isSuccess===true?`el-icon-circle-check show-result-tip-s`:`el-icon-circle-close show-result-tip-b`"></i>
      </template>
    </el-table-column>
  </el-table>
            </div>
        </div>
    </div>
</template>
<script>
import '../dangan.scss'
import {FileApi} from '../api.js'
export default {
    data(){
        return{
            fileList:[],
            selectFile:'选择文件',
            clearFileIsShow:false,
            linkA:'link-click',
            showResult :false,
            successCount: 0,
            badCount:0,
            result:'',
        }
    },
    methods:{
        //点击下载模板
        downloadModel(){
            FileApi.downloadModel().then(res=>{
                //window.open("http://"+res.data.content)
                window.URL.revokeObjectURL(res.data);
            })
        },
        //删除文件
        clearFile(file, fileList) {
            this.$refs.upload.clearFiles();
            this.selectFile = "选择文件"
            this.clearFileIsShow = false
        },
        //文件改变
        handleChangeFile(file, fileList) {
            this.fileList = fileList.slice(-1);
            this.selectFile = file.name
            this.clearFileIsShow = true
        },
        importData(){
            //判断文件类型
            var ext,idx
            idx = this.selectFile.lastIndexOf(".");
            if(idx != -1){
                ext = this.selectFile.substr(idx+1).toUpperCase();
                ext = ext.toLowerCase();
                if(ext!="xlsx"){
                this.$message.error('请选择.xlsx文件！');
                return;
                }
            }else{
                this.$message.error('请选择.xlsx文件！');
                return;
            }
            //上传文件部分
            var files = [];
            this.fileList.forEach(file => {
                files.push(file.raw);
            });
            FileApi.addMulitDeparment({},files).then(res=>{
                this.showResult = true
                this.successCount = res.data.data.successCount
                this.badCount = res.data.data.badCount
                this.result = res.data.data.data
            })
        }
    }
}
</script>
<style lang="scss" scoped>

  
</style>


