﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Rendering_RenderStateBlock : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock o;
			UnityEngine.Rendering.RenderStateMask a1;
			a1 = (UnityEngine.Rendering.RenderStateMask)LuaDLL.luaL_checkinteger(l, 2);
			o=new UnityEngine.Rendering.RenderStateBlock(a1);
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int op_Equality(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock a1;
			checkValueType(l,1,out a1);
			UnityEngine.Rendering.RenderStateBlock a2;
			checkValueType(l,2,out a2);
			var ret=(a1==a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int op_Inequality(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock a1;
			checkValueType(l,1,out a1);
			UnityEngine.Rendering.RenderStateBlock a2;
			checkValueType(l,2,out a2);
			var ret=(a1!=a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_blendState(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.blendState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_blendState(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.BlendState v;
			checkValueType(l,2,out v);
			self.blendState=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rasterState(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.rasterState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rasterState(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.RasterState v;
			checkValueType(l,2,out v);
			self.rasterState=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_depthState(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.depthState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_depthState(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.DepthState v;
			checkValueType(l,2,out v);
			self.depthState=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_stencilState(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.stencilState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_stencilState(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.StencilState v;
			checkValueType(l,2,out v);
			self.stencilState=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_stencilReference(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.stencilReference);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_stencilReference(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			int v;
			checkType(l,2,out v);
			self.stencilReference=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mask(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.mask);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mask(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderStateBlock self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.RenderStateMask v;
			v = (UnityEngine.Rendering.RenderStateMask)LuaDLL.luaL_checkinteger(l, 2);
			self.mask=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Rendering.RenderStateBlock");
		addMember(l,op_Equality);
		addMember(l,op_Inequality);
		addMember(l,"blendState",get_blendState,set_blendState,true);
		addMember(l,"rasterState",get_rasterState,set_rasterState,true);
		addMember(l,"depthState",get_depthState,set_depthState,true);
		addMember(l,"stencilState",get_stencilState,set_stencilState,true);
		addMember(l,"stencilReference",get_stencilReference,set_stencilReference,true);
		addMember(l,"mask",get_mask,set_mask,true);
		createTypeMetatable(l,constructor, typeof(UnityEngine.Rendering.RenderStateBlock),typeof(System.ValueType));
	}
}
