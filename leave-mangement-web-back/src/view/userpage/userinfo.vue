<template>
    <el-card style="width:82%" class="box-card">
        <div>
            <el-col :span="20" style="padding-right:30px;">
                <div class="userinfo-title">
                    <hr/>
                    <p>个人信息</p>
                </div>
                <div class="userinfo-body">
                    <div class="userinfo-body-part">
                        <p>姓名：</p>
                        <span>{{data.name}}</span>
                        <span class="auth" style="color:#5fb878;" v-if="data.isAuth === true"><i class="el-icon-success"></i>已认证</span>
                        <span class="auth" v-if="data.isAuth === false" style="color:red;"><i class="el-icon-warning"></i>未认证</span>
                    </div>
                    <div class="userinfo-body-part">
                        <p>性别：</p>
                        <span>{{data.sex}}</span>
                    </div>
                </div>
                <div class="userinfo-body">
                    <div class="userinfo-body-part">
                        <p>年龄：</p>
                        <span>{{data.age}}</span>
                    </div>
                    <div class="userinfo-body-part">
                        <p>出生日期：</p>
                        <span>{{data.birth | formatDate}}</span>
                    </div>
                </div>
                <div class="userinfo-body">
                    <div class="userinfo-body-part">
                        <p>证件号码：</p>
                        <span>{{data.paperNumber}}</span>
                        <span class="userinfo-body-part-paper-type">{{data.paperType}}</span>
                    </div>
                    <div class="userinfo-body-part">
                        <p>联系电话：</p>
                        <span>{{data.phoneNumber}}</span>
                    </div>
                </div>
                <div class="userinfo-body">
                    <div class="userinfo-body-part">
                        <p>家庭住址：</p>
                        <span>{{data.address}}</span>
                        </div>
                </div>
                <div class="userinfo-body">
                    <div class="userinfo-body-part">
                        <p>公司名称：</p>
                        <span>{{data.company}}</span>
                        </div>
                    <div class="userinfo-body-part">
                        <p>所在部门：</p>
                        <span>{{data.deparment}}</span>
                    </div>
                </div>
                <div class="userinfo-body">
                    <div class="userinfo-body-part">
                        <p>职位：</p>
                        <span>{{data.position}}</span>
                        </div>
                    <div class="userinfo-body-part">
                        <p>员工状态：</p>
                        <span>{{data.state}}</span>
                    </div>
                </div>
                <div v-if="show">
                    <div class="userinfo-title">
                        <hr/>
                        <p>{{title}}</p>
                    </div>
                    <comp-message v-if="showBox==='message'" @closeForm="closeForm" :workerId="userInfo.id"></comp-message>
                    <comp-password v-if="showBox==='password'" @closeForm="closeForm" :workerId="userInfo.id"></comp-password>
                </div>
            </el-col>
            <el-col :span="4">
                <div class="userinfo-userimg">
                    <comp-upload-img></comp-upload-img>
                </div>
                <div class="userinfo-btn">
                    <ul>
                        <li><el-button @click="handleCompleteMessage()" type="primary" size="mini">完善资料</el-button></li>
                        <!-- <li><el-button @click="handleAuth()" type="primary" size="mini">个人认证</el-button></li> -->
                        <li><el-button @click="handleModifyPassword()" type="primary" size="mini">修改密码</el-button></li>
                    </ul>
                </div>
            </el-col>
        </div>
    </el-card>
</template>
<script>
import './userpage.scss'
import {mapGetters} from 'vuex'
import { UserPageApi } from "./api.js";
import CompPassword from './modifypassword'
import CompMessage from './workermessage'
import CompUploadImg from '@/packages/components/upload-file'
export default {
    components:{
        CompPassword,
        CompMessage,
        CompUploadImg
    },
    data(){
        return{
            data:{},
            title:'',
            show:false,
            showBox:'',
        }
    },
    computed: {
            ...mapGetters([
                'userInfo'
            ])
        },
    mounted(){
        this.loadUserinfo()
    },
    methods:{
        loadUserinfo(){
            UserPageApi.getWorkerMessage(this.userInfo.id).then(res=>{
                this.data = res.data
            })
        },
        //点击完善信息
        handleCompleteMessage(){
            this.changeShowBox("完善个人资料","message")
        },
        //点击认证
        handleAuth(){
            this.changeShowBox("员工身份认证","auth")
        },
        //点击修改密码
        handleModifyPassword(){
            this.changeShowBox("修改密码","password")
        },
        changeShowBox(title,part){
            this.title = title
            this.showBox = part
            this.show = true
        },
        closeForm(value){
            this.loadUserinfo()
            this.show = value
        }
    },
    filters: {
            formatDate: function (value) {
                let date = new Date(value);
                let y = date.getFullYear();
                let MM = date.getMonth() + 1;
                MM = MM < 10 ? ('0' + MM) : MM;
                let d = date.getDate();
                d = d < 10 ? ('0' + d) : d;
                return y + '-' + MM + '-' + d;
                }
            }
}
</script>

