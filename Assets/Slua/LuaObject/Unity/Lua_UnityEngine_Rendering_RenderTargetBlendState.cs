﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Rendering_RenderTargetBlendState : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState o;
			UnityEngine.Rendering.ColorWriteMask a1;
			a1 = (UnityEngine.Rendering.ColorWriteMask)LuaDLL.luaL_checkinteger(l, 2);
			UnityEngine.Rendering.BlendMode a2;
			a2 = (UnityEngine.Rendering.BlendMode)LuaDLL.luaL_checkinteger(l, 3);
			UnityEngine.Rendering.BlendMode a3;
			a3 = (UnityEngine.Rendering.BlendMode)LuaDLL.luaL_checkinteger(l, 4);
			UnityEngine.Rendering.BlendMode a4;
			a4 = (UnityEngine.Rendering.BlendMode)LuaDLL.luaL_checkinteger(l, 5);
			UnityEngine.Rendering.BlendMode a5;
			a5 = (UnityEngine.Rendering.BlendMode)LuaDLL.luaL_checkinteger(l, 6);
			UnityEngine.Rendering.BlendOp a6;
			a6 = (UnityEngine.Rendering.BlendOp)LuaDLL.luaL_checkinteger(l, 7);
			UnityEngine.Rendering.BlendOp a7;
			a7 = (UnityEngine.Rendering.BlendOp)LuaDLL.luaL_checkinteger(l, 8);
			o=new UnityEngine.Rendering.RenderTargetBlendState(a1,a2,a3,a4,a5,a6,a7);
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
			UnityEngine.Rendering.RenderTargetBlendState a1;
			checkValueType(l,1,out a1);
			UnityEngine.Rendering.RenderTargetBlendState a2;
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
			UnityEngine.Rendering.RenderTargetBlendState a1;
			checkValueType(l,1,out a1);
			UnityEngine.Rendering.RenderTargetBlendState a2;
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
	static public int get_defaultValue(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.RenderTargetBlendState.defaultValue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_writeMask(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.writeMask);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_writeMask(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.ColorWriteMask v;
			v = (UnityEngine.Rendering.ColorWriteMask)LuaDLL.luaL_checkinteger(l, 2);
			self.writeMask=v;
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
	static public int get_sourceColorBlendMode(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.sourceColorBlendMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_sourceColorBlendMode(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.BlendMode v;
			v = (UnityEngine.Rendering.BlendMode)LuaDLL.luaL_checkinteger(l, 2);
			self.sourceColorBlendMode=v;
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
	static public int get_destinationColorBlendMode(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.destinationColorBlendMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_destinationColorBlendMode(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.BlendMode v;
			v = (UnityEngine.Rendering.BlendMode)LuaDLL.luaL_checkinteger(l, 2);
			self.destinationColorBlendMode=v;
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
	static public int get_sourceAlphaBlendMode(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.sourceAlphaBlendMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_sourceAlphaBlendMode(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.BlendMode v;
			v = (UnityEngine.Rendering.BlendMode)LuaDLL.luaL_checkinteger(l, 2);
			self.sourceAlphaBlendMode=v;
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
	static public int get_destinationAlphaBlendMode(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.destinationAlphaBlendMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_destinationAlphaBlendMode(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.BlendMode v;
			v = (UnityEngine.Rendering.BlendMode)LuaDLL.luaL_checkinteger(l, 2);
			self.destinationAlphaBlendMode=v;
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
	static public int get_colorBlendOperation(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.colorBlendOperation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_colorBlendOperation(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.BlendOp v;
			v = (UnityEngine.Rendering.BlendOp)LuaDLL.luaL_checkinteger(l, 2);
			self.colorBlendOperation=v;
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
	static public int get_alphaBlendOperation(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.alphaBlendOperation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_alphaBlendOperation(IntPtr l) {
		try {
			UnityEngine.Rendering.RenderTargetBlendState self;
			checkValueType(l,1,out self);
			UnityEngine.Rendering.BlendOp v;
			v = (UnityEngine.Rendering.BlendOp)LuaDLL.luaL_checkinteger(l, 2);
			self.alphaBlendOperation=v;
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
		getTypeTable(l,"UnityEngine.Rendering.RenderTargetBlendState");
		addMember(l,op_Equality);
		addMember(l,op_Inequality);
		addMember(l,"defaultValue",get_defaultValue,null,false);
		addMember(l,"writeMask",get_writeMask,set_writeMask,true);
		addMember(l,"sourceColorBlendMode",get_sourceColorBlendMode,set_sourceColorBlendMode,true);
		addMember(l,"destinationColorBlendMode",get_destinationColorBlendMode,set_destinationColorBlendMode,true);
		addMember(l,"sourceAlphaBlendMode",get_sourceAlphaBlendMode,set_sourceAlphaBlendMode,true);
		addMember(l,"destinationAlphaBlendMode",get_destinationAlphaBlendMode,set_destinationAlphaBlendMode,true);
		addMember(l,"colorBlendOperation",get_colorBlendOperation,set_colorBlendOperation,true);
		addMember(l,"alphaBlendOperation",get_alphaBlendOperation,set_alphaBlendOperation,true);
		createTypeMetatable(l,constructor, typeof(UnityEngine.Rendering.RenderTargetBlendState),typeof(System.ValueType));
	}
}
